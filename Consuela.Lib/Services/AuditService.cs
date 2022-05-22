using Consuela.Entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace Consuela.Lib.Services
{
    public class AuditService 
        : IAuditService
    {
        private readonly IProfile _profile;
        private readonly IFileService _fileService;
        private readonly List<string> _logs;

        public AuditService(IProfile profile, IFileService fileService)
        {
            _profile = profile;
            
            _fileService = fileService;

            _logs = new List<string>();
        }

        public void Log(string message) => Add(message);

        public void Log(FileInfoEntity file) => Add($"File,{file.CreationTime:yyyy/MM/dd HH:mm:ss},{file.DirectoryName},{file.Name}");

        public void Log(Exception exception) => Add(exception.ToString());

        private void Add(string message)
        {
            Console.WriteLine(message);

            _logs.Add(message);
        }
        
        public override string ToString()
        {
            var n = Environment.NewLine;

            var header = $"{DateTime.Now:yyyy.MM.dd HH:mm:ss}{n}===================================================={n}";

            var body = string.Join(n, _logs);

            var final = header + body + n + n;

            return final;
        }

        public void SaveLog()
        {
            var path = Path.Combine(_profile.Logging.Path, "Delete operations.log");

            _fileService.AppendAllText(path, ToString());
        }
    }
}
