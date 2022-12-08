using Consuela.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Consuela.Lib.Services
{
    public class AuditService 
        : IAuditService
    {
        private readonly IProfile _profile;
        private readonly IFileService _fileService;
        private readonly IExcelFileWriterService _excelFileWriterService;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILogger _logger;
        private readonly List<string> _plainTextLog;
        private readonly List<AuditRow> _auditRows;
        private DateTime _now;

        public AuditService(
            IProfile profile, 
            IFileService fileService,
            IExcelFileWriterService excelFileWriterService,
            IDateTimeService dateTimeService,
            ILogger<AuditService> logger)
        {
            _profile = profile;
            
            _fileService = fileService;

            _excelFileWriterService = excelFileWriterService;

            _dateTimeService = dateTimeService;

            _logger = logger;

            _plainTextLog = new List<string>();

            _auditRows = new List<AuditRow>();

            Reset(); //Initializing variables for first run
        }

        public void LogDirectory(string path) => Add(new AuditRow
        {
            FileType = "Directory",
            CreationTime = null,
            Path = path,
            Filename = null
        });

        public void LogFile(FileInfoEntity file) => Add(new AuditRow
        {
            FileType = "File",
            CreationTime = file.CreationTime,
            Path = file.DirectoryName,
            Filename = file.Name
        });

        private void Add(AuditRow auditRow)
        {
            //For excel
            _auditRows.Add(auditRow);

            var message = $"{auditRow.FileType},{auditRow.CreationTime:yyyy/MM/dd HH:mm:ss},{auditRow.Path},{auditRow.Filename}";

            //To see live
            _logger.LogInformation(message);

            //For the text log version
            _plainTextLog.Add(message);
        }
        
        public override string ToString()
        {
            var n = Environment.NewLine;

            var header = $"{_dateTimeService.Now:yyyy.MM.dd HH:mm:ss}{n}===================================================={n}";

            var body = string.Join(n, _plainTextLog);

            var final = header + body + n + n;

            return final;
        }

        /// <summary>
        /// Must be called in between separate Clean Up Service runs to ensure that
        /// stateful variables are reset.
        /// </summary>
        public void Reset()
        {
            _plainTextLog.Clear();
            
            _auditRows.Clear();

            _now = _dateTimeService.Now;

            //Just in case the path doesn't exist, attempt to create it
            _fileService.CreateDirectory(_profile.Audit.Path);

            PurgeExpiredLogs();
        }

        public void SaveLog()
        {
            var path = GetTimestampedFullFilePath("Delete operations audit.log");

            try
            {
                //Excel stuff can be weird sometimes, so wrapping in try/catch
                SaveExcelFile();
            }
            catch (Exception ex)
            {
                //To see live
                _logger.LogError(ex, "Couldn't save excel file log.");

                //Log the problem somewhere to see it.
                _plainTextLog.Add($"Couldn't save excel file log. Error:{Environment.NewLine}{ex}");
            }

            //If the file doesn't exist, it will be created
            _fileService.AppendAllText(path, ToString());
        }

        private void SaveExcelFile()
        {
            var path = GetTimestampedFullFilePath("Delete audit.xlsx");

            var data = new ExcelSheetData
            {
                Headers = new List<string> { "File Type", "Created On", "Path", "Filename" },
                SheetName = "Delete audit"
            };

            data.RowData = new object[_auditRows.Count, data.Headers.Count];

            //Transfer the audit rows to the excel range
            for (int i = 0; i < _auditRows.Count; i++)
            {
                var a = _auditRows[i];

                data.RowData[i, 0] = a.FileType;
                data.RowData[i, 1] = a.CreationTime;
                data.RowData[i, 2] = a.Path;
                data.RowData[i, 3] = a.Filename;
            }

            _excelFileWriterService.SaveAs(data, path);
        }

        private void PurgeExpiredLogs()
        {
            try
            {
                //This is internal clean up so I am not going to bother making it configurable
                var files = _fileService.GetFiles(new PathAndPattern(_profile.Audit.Path, "*"), 30);

                files.ForEach(_fileService.DeleteFileIfExists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failure during log purge. 0x202207210038");
            }
        }

        //Automatically a rolling audit file because it's time based
        private string GetTimestampedFullFilePath(string fileNameSuffix)
            => Path.Combine(_profile.Audit.Path, $"{_now:yyyy.MM.dd_HH.mm.ss} {fileNameSuffix}");

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
                DirectoryDeleteErrors = results.DirectoryDeleteErrors.ToDictionary(k => k.Key, v => new ExLog(v.Value)),
                FileDeleteErrors = results.FileDeleteErrors.ToDictionary(k => k.Key.FullName, v => new ExLog(v.Value))
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

        //This is just to avoid trying to serialize objects that aren't serializable such as IntPnt which can show up in the Target property.
        private class ExLog
        {
            public ExLog(Exception ex)
            {
                Type = ex.GetType().Name;

                Message = ex.Message;

                Stacktrace = ex.StackTrace;
            }

            public string Type { get; set; }
            
            public string Message { get; set; }
            
            public string Stacktrace { get; set; }
        }
    }
}
