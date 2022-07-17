using Consuela.Entity;
using System;

namespace Consuela.Lib.Services
{
    public interface IAuditService
    {
        void Reset();

        void Log(string message);

        void Log(FileInfoEntity file);

        void Log(Exception exception);

        void SaveLog();

        void SaveStatistics(CleanUpResults results);

        string ToString();
    }
}