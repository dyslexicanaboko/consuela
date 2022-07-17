using Consuela.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Consuela.Lib.Services
{
    public class AuditService 
        : IAuditService
    {
        private readonly IProfile _profile;
        private readonly IFileService _fileService;
        private readonly IDateTimeService _dateTimeService;
        private readonly List<string> _logs;
        private DateTime _now;

        public AuditService(
            IProfile profile, 
            IFileService fileService,
            IDateTimeService dateTimeService)
        {
            _profile = profile;
            
            _fileService = fileService;
            
            _dateTimeService = dateTimeService;

            _logs = new List<string>();

            Reset(); //Initializing variables for first run
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

            var header = $"{_dateTimeService.Now:yyyy.MM.dd HH:mm:ss}{n}===================================================={n}";

            var body = string.Join(n, _logs);

            var final = header + body + n + n;

            return final;
        }

        /// <summary>
        /// Must be called in between separate Clean Up Service runs to ensure that
        /// stateful variables are reset.
        /// </summary>
        public void Reset()
        {
            _logs.Clear();

            _now = _dateTimeService.Now;

            //Just in case the path doesn't exist, attempt to create it
            _fileService.CreateDirectory(_profile.Audit.Path);

            //TODO: Automatic clean up of rolling audit files in separate method
            //Can't do this until a protection is offered where the Logging directory is NOT the EXE directory!
        }

        public void SaveLog()
        {
            var path = GetTimestampedFullFilePath("Delete operations audit.log");

            //If the file doesn't exist, it will be created
            _fileService.AppendAllText(path, ToString());

        }

        //Automatically a rolling audit file because it's time based
        private string GetTimestampedFullFilePath(string fileNameSuffix)
            => Path.Combine(_profile.Audit.Path, $"{_now:yyyy.MM.dd}{fileNameSuffix}");

        public void SaveStatistics(CleanUpResults results)
        {
            //Using an anonymous object because it won't be used anywhere else
            var stats = new
            {
                CreatedOn = _now,
                Counts = new
                {
                    FileDeleteErrors = results.FileDeleteErrors.Count,
                    DirectoryDeleteErrors = results.DirectoryDeleteErrors.Count,
                    DirectoriesDeleted = results.DirectoriesDeleted.Count,
                    FilesDeleted = results.FilesDeleted.Count,
                    FilesIgnored = results.FilesIgnored.Count
                },
                results.DirectoryDeleteErrors,
                results.FileDeleteErrors
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var serialized = JsonSerializer.Serialize(stats, options);

            var path = GetTimestampedFullFilePath("Statistics and Errors.json");

            //If the file doesn't exist, it will be created
            _fileService.AppendAllText(path, serialized);
        }
    }
}
