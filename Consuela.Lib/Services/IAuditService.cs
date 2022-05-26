using Consuela.Entity;
using System;

namespace Consuela.Lib.Services
{
    public interface IAuditService
    {
        void Log(string message);

        void Log(FileInfoEntity file);

        void Log(Exception exception);

        void SaveLog();

        string ToString();
    }
}