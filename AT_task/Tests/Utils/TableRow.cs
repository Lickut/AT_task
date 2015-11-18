using System;

namespace AT_task
{
	public class TableRow
	{
		public string Name { get; set;}
		public string Company{ get; set;}
		public DateTime HireDate{ get; set;}
		public TableRow (string name, string company,DateTime hireDate)
		{
			Name = name;
			Company = company;
			HireDate = hireDate;
		}

		public TableRow(string name, string company,string hireDate){
			Name = name;
			Company = company;
			HireDate= Convert.ToDateTime(hireDate);
	}
}

