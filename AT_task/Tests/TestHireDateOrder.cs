using NUnit.Framework;
using System;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace AT_task
{
	[TestFixture]
	public class TestHireDateOrder
	{
		private IWebDriver driver;
		private TablePage page;
		private string tablePageURL = "/path/to/html/page/with/tables";

		[TestFixtureSetUp]
		public void openDriver ()
		{
			driver = new FirefoxDriver ();
		}

		[SetUp]
		public void Init ()
		{
			driver.Navigate ().GoToUrl (tablePageURL);
			page = new TablePage (driver);
		}

		[TestCase ("sortedAscendingOneRow", "Asc", "Hire Date", true)]
		[TestCase ("sortedAscendingEmpty", "Asc", "Hire Date", true)]
		[TestCase ("sortedAscendingWithSameValues", "Asc", "Hire Date", true)]
		[TestCase ("sortedAscendingWithDifferentValues", "Asc", "Hire Date", true)]
		[TestCase ("sortedDescendingOneRow", "Desc", "Hire Date", true)]
		[TestCase ("sortedDescendingEmptyTable", "Desc", "Hire Date", true)]
		[TestCase ("sortedDescendingWithSameValues", "Desc", "Hire Date", true)]
		[TestCase ("sortedDescendingWithDifferentValues", "Desc", "Hire Date", true)]
		[TestCase ("notSortedAscending", "Asc", "Hire Date", false)]
		[TestCase ("notSortedDescending", "Desc", "Hire Date", false)]
		public void TestTableIsOrdered (string tableID, string orderType,string columnName, bool isSorted)
		{
			IList<DateTime> hireDates = GetHireDatesFromTable (page.getTableRows (tableID), columnName);
			switch (orderType) {
			case "Asc":
				Assert.AreEqual (isSorted, OrderUtils.IsSorted (hireDates, new sortDateAscending ()));
				break;
			case "Desc":
				Assert.AreEqual (isSorted, OrderUtils.IsSorted (hireDates, new sortDateDescending ()));
				break;
			}
		}

		[TestFixtureTearDown]
		public void TearDown ()
		{
			driver.Quit ();
		}

		public IList<DateTime> GetHireDatesFromTable (IList<IList<string>> table, string columnName)
		{
			int hireDateIndex = table [0].IndexOf (columnName);
			IList<DateTime> hireDates = new List<DateTime> ();
			for (int i = 1; i < table.Count; i++) {
				hireDates.Add (Convert.ToDateTime (table [i] [hireDateIndex]));
			}
			return hireDates;
		}


	}
}

