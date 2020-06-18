using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace FindString
{
	public class StringQuery
	{
		private readonly StringBuilder _results = new StringBuilder();
		private readonly Queue<string> _arguments;
		private bool _matchAtLineBeginning;
		private bool _matchAtLineEnd;
		private IList<string> _strings = new List<string>();
		private IList<string> _files = new List<string>();

		public QueryStatus Status { get; private set; } = QueryStatus.Unknown;

		public StringQuery(IEnumerable<string> arguments)
		{
			arguments ??= new Queue<string>(new[] { "/?" });
			_arguments = new Queue<string>(arguments);
		}


		public string ExecuteQuery()
		{
			ParseArguments();
			CheckParsingResult();
			return _results.ToString();
		}

		private void ParseArguments()
		{
			if (_arguments.Count == 0)
			{
				Status = QueryStatus.PrintHelp;
				return;
			}

			while (_arguments.TryDequeue(out var rawArgument))
			{
				if (rawArgument.Length < Helpers.MinimalNumberOfArguments)
				{
					Status = QueryStatus.BadArguments;
					return;
				}
				if (rawArgument[0] == '/')
				{
					var subArgument = rawArgument[1..].Split(':');
					if (subArgument[0].Length > 1 && subArgument[0][..2] != "off")
					{
						Status = QueryStatus.BadArguments;
						return;
					}
					switch (subArgument[0].ToLowerInvariant())
					{
						case "b":
							_matchAtLineBeginning = true;
							break;
						case "c":
							if (string.IsNullOrWhiteSpace(subArgument[1]))
							{
								Status = QueryStatus.ArgumentMissingAfterC;
								return;
							}
							IList<string> stringPhase = new List<string>
							{
								subArgument[1]
							};
							while (_arguments.TryDequeue(out var result))
							{
								ExtractStringPhase(result, ref stringPhase);
							}
							_strings.Add(string.Join(' ', stringPhase));
							break;
						case "f":
							_files = _files.Union(ReadEntriesFromFile(subArgument[1])).ToList();
							break;
						case "g":
							_strings = _strings.Union(ReadEntriesFromFile(subArgument[1])).ToList();
							break;
						case "e":
							_matchAtLineEnd = false;
							break;
						case "?":
							Status = QueryStatus.PrintHelp;
							break;
						default:
							Status = QueryStatus.BadArguments;
							return;
					}
				}
				else
				{
					ExtractStringPhase(rawArgument, ref _strings);
				}
			}

			if (_files.Count > Helpers.MinimalNumberOfParameters && _strings.Count > Helpers.MinimalNumberOfParameters)
			{
				Status = QueryStatus.Valid;
			}

			void ExtractStringPhase(string rawArgument, ref IList<string> list)
			{
				if (_arguments.Count == 0)
				{
					_files.Add(rawArgument);
					return;
				}

				list.Add(rawArgument);
			}
		}

		private IEnumerable<string> ReadEntriesFromFile(string s)
		{
			try
			{
				return File.ReadAllLines($@"{s}");
			}
			catch
			{
				// TODO: Check if the file is online/offline and take into account the switch.
				_results.AppendLine($"Can't open file {Path.GetFileName(s)}");
				return new string[0];
			}
		}

		private void CheckParsingResult()
		{
			switch (Status)
			{
				case QueryStatus.Valid:
					FindResult();
					return;
				case QueryStatus.PrintHelp:
					_results.Append(Helpers.HelpMessage);
					return;
				case QueryStatus.Invalid:
				case QueryStatus.Unknown:
				case QueryStatus.BadArguments:
					_results.Append(Helpers.ErrorMessage);
					return;
				case QueryStatus.ArgumentMissingAfterC:
					_results.Append(Helpers.ErrorMessage);
					return;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void FindResult()
		{
			foreach (var file in _files)
			{
				// TODO: Check non-printable characters: To do so, we have to read a file as a byte stream and check if it is binary or not.
				// TODO: This is an heuristic process, not deterministic.
				// TODO: There is 1:2^16 (about 0.0015 %) chance that binary file will be interpret as UTF-8 BOM.
				// TODO: There is 1:2^24 chance that binary file will be interpret as UTF-16 BOM.
				// TODO: For UTF-8 vs. ASCII + code page, we have implement UTF-8 decoder and validate all bytes for a valid UTF-8 sequence.
				// TODO: Quite a lot of work. :(
				foreach (var line in ReadEntriesFromFile(file))
				{

				}
			}
		}
	}
}