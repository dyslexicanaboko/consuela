using Consuela.Entity;

namespace Consuela.Lib.Services
{
    public interface IAuditService
    {
        void Reset();

        void LogDirectory(string path);

        void LogFile(FileInfoEntity file);

        void SaveLog();

        void SaveStatistics(CleanUpResults results);
    }
}