using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindString;


namespace FindString.Tests
{
	[TestClass]
	public class StringQueryTests
	{
		[TestClass]
		public class ExecuteQueryTests
		{
			[TestCategory(TestCategory.Basic)]
			[TestMethod]
			public void NoArgumentTest()
			{
				var stringQuery = new StringQuery(new string[0]);
				Assert.AreEqual(Helpers.HelpMessage, stringQuery.ExecuteQuery());
			}

			[TestCategory(TestCategory.Basic)]
			[TestMethod]
			public void EmptyArgumentTest()
			{
				var stringQuery = new StringQuery(new[] { "" });
				Assert.AreEqual(Helpers.ErrorMessage, stringQuery.ExecuteQuery());
			}

			[TestCategory(TestCategory.Basic)]
			[TestMethod]
			public void NullAsArgumentTest()
			{
				var stringQuery = new StringQuery(null);
				Assert.AreEqual(Helpers.HelpMessage, stringQuery.ExecuteQuery());
			}


			[TestCategory(TestCategory.Basic)]
			[TestMethod]
			public void OneArgumentTest()
			{
				var stringQuery = new StringQuery(new[] { "asdf" });
				Assert.AreEqual(Helpers.ErrorMessage, stringQuery.ExecuteQuery());
			}

			[TestCategory(TestCategory.Basic)]
			[TestMethod]
			public void UnknownArgumentTest()
			{
				var stringQuery = new StringQuery(new[] { "/z" });
				Assert.AreEqual(Helpers.ErrorMessage, stringQuery.ExecuteQuery());
			}

			[TestCategory(TestCategory.Basic)]
			[TestMethod]
			public void PrintHelpTest()
			{
				var stringQuery = new StringQuery(new[] { "/?" });
				Assert.AreEqual(Helpers.HelpMessage, stringQuery.ExecuteQuery());
			}

			[TestCategory(TestCategory.Basic)]
			[TestMethod]
			public void MultipleCharWithColonAsArgumentTest()
			{
				var stringQuery = new StringQuery(new[] { "/aa:" });
				Assert.AreEqual(Helpers.ErrorMessage, stringQuery.ExecuteQuery());
			}

		}
	}
}