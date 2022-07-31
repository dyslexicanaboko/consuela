namespace Consuela.Entity
{
	public class ExcelSheetData
	{
		public List<string> Headers { get; set; }

		public object[,] RowData { get; set; }

		public string SheetName { get; set; }
	}
}
