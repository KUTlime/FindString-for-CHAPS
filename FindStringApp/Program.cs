using System;

using FindString;

namespace FindStringApp
{
	// https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/findstr
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(new StringQuery(args).ExecuteQuery());
			Console.ReadKey();
		}
	}
}

