using Consuela.Entity;
using System;

namespace Consuela.Lib.Services
{
    public interface ILoggingService
    {
        void Log(string message);

        void Log(FileInfoEntity file);

        void Log(Exception exception);

        string ToString();
    }
}