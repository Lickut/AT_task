using NUnit.Framework;
using System;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace AT_task
{
	[TestFixture]
	public class TestHireDateOrder
	{
		private IWebDriver driver;
		private TablePage page;
		private string tablePageURL = "file:///C:/Users/Denys_Verotskyi/Documents/Projects/test/test/Tests/Resources/table_example.html";

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

		[TestCase ("sortedAscendingOneRow", "Asc", "Hire Date", "dd-MM-yyyy", true)]
		[TestCase ("sortedAscendingEmpty", "Asc", "Hire Date", "dd-MM-yyyy", true)]
		[TestCase ("sortedAscendingWithSameValues", "Asc", "Hire Date", "dd-MM-yyyy", true)]
		[TestCase ("sortedAscendingWithDifferentValues", "Asc", "Hire Date", "dd-MM-yyyy", true)]
		[TestCase ("sortedDescendingOneRow", "Desc", "Hire Date", "dd-MM-yyyy", true)]
		[TestCase ("sortedDescendingEmptyTable", "Desc", "Hire Date", "dd-MM-yyyy", true)]
		[TestCase ("sortedDescendingWithSameValues", "Desc", "Hire Date", "dd-MM-yyyy", true)]
		[TestCase ("sortedDescendingWithDifferentValues", "Desc", "Hire Date", "dd-MM-yyyy", true)]
		[TestCase ("notSortedAscending", "Asc", "Hire Date", "dd-MM-yyyy", false)]
		[TestCase ("notSortedDescending", "Desc", "Hire Date", "dd-MM-yyyy", false)]
		public void TestTableIsOrdered (string tableID, string orderType, string columnName, string dateFormat, bool isSorted)
		{
			IList<string> columnStringDates = GetColumnDataFromTable (page.getTableRows (tableID), columnName);
			if (columnStringDates.Count != 0) {
				var columnDates = columnStringDates.Select (x => DateTime.ParseExact (x, dateFormat, CultureInfo.InvariantCulture)).ToList ();
				switch (orderType) {
				case "Asc":
					Assert.AreEqual (isSorted, OrderUtils.IsSorted (columnDates, new sortDateAscending ()));
					break;
				case "Desc":
					Assert.AreEqual (isSorted, OrderUtils.IsSorted (columnDates, new sortDateDescending ()));
					break;
				}
			}
		}

		[TestFixtureTearDown]
		public void TearDown ()
		{
			driver.Quit ();
		}

		public IList<string> GetColumnDataFromTable (IList<IList<string>> table, string columnName)
		{
			int columnIndex = table [0].IndexOf (columnName);
			IList<string> column = new List<string> ();
			for (int i = 1; i < table.Count; i++) {
				column.Add (table [i] [columnIndex]);
			}
			return column;
		}


	}
}
