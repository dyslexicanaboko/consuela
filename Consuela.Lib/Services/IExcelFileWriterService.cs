using Consuela.Entity;

namespace Consuela.Lib.Services
{
    public interface IExcelFileWriterService
    {
        void SaveAs(ExcelSheetData reportData, string saveAs);
    }
}