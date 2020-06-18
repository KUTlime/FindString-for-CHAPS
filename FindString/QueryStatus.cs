namespace FindString
{
	public enum QueryStatus : byte
	{
		Unknown = 0,
		Valid = 1,
		Invalid = 2,
		PrintHelp = 3,
		BadArguments = 4,
		ArgumentMissingAfterC = 5,
	}
}