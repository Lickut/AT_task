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
		private string hireDateColumnName = "Hire Date";

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

		[TestCase ("sortedAscending1", "Asc", true)]
		[TestCase ("sortedAscending2", "Asc", true)]
		[TestCase ("sortedAscending3", "Asc", true)]
		[TestCase ("sortedAscending4", "Asc", true)]
		[TestCase ("sortedDescending1", "Desc", true)]
		[TestCase ("sortedDescending2", "Desc", true)]
		[TestCase ("sortedDescending3", "Desc", true)]
		[TestCase ("sortedDescending4", "Desc", true)]
		[TestCase ("notSortedAscending1", "Asc", false)]
		[TestCase ("notSortedDescending1", "Desc", false)]
		public void TestTableIsOrdered (string tableID, string orderType, bool isSorted)
		{
			IList<DateTime> hireDates = GetHireDatesFromTable (page.getTableRows (tableID));
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

		public IList<DateTime> GetHireDatesFromTable (IList<IList<string>> table)
		{
			int hireDateIndex = table [0].IndexOf (hireDateColumnName);
			IList<DateTime> hireDates = new List<DateTime> ();
			for (int i = 1; i < table.Count; i++) {
				hireDates.Add (Convert.ToDateTime (table [i] [hireDateIndex]));
			}
			return hireDates;
		}


	}
}

