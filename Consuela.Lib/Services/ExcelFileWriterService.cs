using Consuela.Entity;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.IO;

namespace Consuela.Lib.Services
{
    //TODO: Parking all of this logic here, but eventually this should move to its own project
    //since I want to keep expanding this library.
    //This is a modified copy of this: https://github.com/dyslexicanaboko/code-snippets/tree/develop/LinqPadScripts/Excel
    public class ExcelFileWriterService : IExcelFileWriterService
    {
        public void SaveAs(ExcelSheetData reportData, string saveAs)
        {
            var d = reportData.RowData;
            var columns = reportData.Headers.Count; //Header count will cover calculated and row data
            //var rowTableFirst = 2; //Header is always 1
            var rowTableLast = d.GetLength(0) + 1;

            //TODO: For now delete the file before continuing. Eventually needs to be re-used
            File.Delete(saveAs);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(saveAs)))
            {
                var wb = package.Workbook;

                var sheet = wb.Worksheets.Add(reportData.SheetName);

                WriteTableHeader(sheet, reportData.Headers);

                WriteTableBody(sheet, d);

                //Change range into table
                var t = sheet.Tables.Add(new ExcelAddressBase(1, 1, rowTableLast, columns), "DeleteAudit");

                t.TableStyle = TableStyles.Medium16;

                AutoSizeColumns(t, 1, 1);

                package.Save();
            }
        }

        private void WriteTableHeader(ExcelWorksheet sheet, IList<string> headers)
        {
            for (var c = 0; c < headers.Count; c++)
            {
                sheet.Cells[1, c + 1].Value = headers[c];
            }
        }

        //TODO: This should be exposed to accept custom instructions
        private void WriteTableBody(ExcelWorksheet sheet, object[,] rowData)
        {
            //Index value
            for (var r = 0; r < rowData.GetLength(0); r++)
            {
                //Offsetting:
                //  +1 for Excel row
                //  +1 for skipping Header row
                var r0 = r + 2;

                //E 1
                Cell(sheet, r0, 1, rowData[r, 0]);
                //E 2
                CellAsDateTime(sheet, r0, 2, rowData[r, 1]);
                //E 3
                Cell(sheet, r0, 3, rowData[r, 2]);
                //E 4
                Cell(sheet, r0, 4, rowData[r, 3]);
            }
        }

        private ExcelRange Cell(ExcelWorksheet sheet, int row, int column, object value = null)
        {
            var c = sheet.Cells[row, column];

            if (value == null) return c;

            c.Value = value;

            return c;
        }

        //https://github.com/pruiz/EPPlus/blob/master/EPPlus/Style/ExcelNumberFormat.cs
        private ExcelRange CellNumberFormat(ExcelRange cell, string format)
        {
            cell.Style.Numberformat.Format = format;

            return cell;
        }

        private ExcelRange CellAsDateTime(ExcelWorksheet sheet, int row, int column, object value = null)
        {
            var c = Cell(sheet, row, column, value);

            c = CellNumberFormat(c, "m/d/yyyy hh:mm:ss");

            return c;
        }

        //This doesn't work perfectly
        //TODO: Find the largest piece of data per column and size it according to that cell.
        private void AutoSizeColumns(ExcelTable table, int row, int columnStart)
        {
            var sheet = table.WorkSheet;

            for (var column = columnStart; column <= table.Columns.Count; column++)
            {
                var c = Cell(sheet, row, column);

                c.AutoFitColumns();
            }
        }
    }
}
