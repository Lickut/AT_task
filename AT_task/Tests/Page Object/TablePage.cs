using System;
using Selenium;
using System.Collections;

namespace AT_task
{
	public class TablePage
	{
		[FindBy(How = How.Xpath, Using = "//td")]
		private IWebElement tables;
		protected IWebDriver driver;

		public TablePage (IWebDriver driver)
		{
			this.driver = driver;
		}

		public IList<TableRow> getTable(){
			IList<IWebElement> rows = driver.FindElements(By.XPath("/tr[preceding-sibling::*]"));

	}
}

