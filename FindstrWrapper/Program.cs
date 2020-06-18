using System;
using System.Diagnostics;

namespace FindstrWrapper
{
	class Program
	{
		static void Main(string[] args)
		{
			Process.Start(@"C:\Windows\system32\findstr.exe", args.Length > 0 ? String.Join(' ', args) : "/?");
			Console.ReadKey();
		}
	}
}
