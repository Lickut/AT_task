using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace AT_task
{
	public class TablePage
	{
		[FindsBy (How = How.Id, Using = "orderedtable")]
		public IWebElement Table{ get; set; }

		protected IWebDriver driver;

		public TablePage (IWebDriver driver)
		{
			this.driver = driver;
			PageFactory.InitElements (driver, this);
		}

		public IList<IList<string>> getTableRows (IWebElement table)
		{
			IList<IWebElement> rowsWeb = table.FindElements (By.TagName ("tr"));
			IList<IList<string>> tableRows = new List<IList<string>> ();
			for (int i = 0; i < rowsWeb.Count; i++) {
				IList<IWebElement> rowCellsWeb = rowsWeb [i].FindElements (By.XPath ("./*"));
				IList<string> rowCells = new List<string> ();
				for (int j = 0; j < rowCellsWeb.Count; j++) {
					rowCells.Add (rowCellsWeb [j].Text);
				}
				tableRows.Add (rowCells);
			}
			return tableRows;
		}

		public IList<IList<string>> getTableRows (string tableID)
		{
			return getTableRows (driver.FindElement (By.Id (tableID)));
		}

		public IList<IList<string>> getTableRows ()
		{
			return getTableRows (Table);
		}
	}
}

