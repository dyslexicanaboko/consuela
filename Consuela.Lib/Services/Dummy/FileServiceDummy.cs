using Consuela.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Consuela.Lib.Services.Dummy
{
    public class FileServiceDummy
        : IFileService
    {
        private readonly IDateTimeService _dateTimeService;

        public FileServiceDummy(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        public List<FileInfoEntity> FilePaths { get; set; } = new List<FileInfoEntity>();
        
        public List<string> Directories { get; set; } = new List<string>();

        public void CreateDirectory(string path) { /* Do nothing */ }

        public void AppendAllText(string path, string contents) { /* Do nothing */ }

        public void DeleteDirectoryIfExists(string path) => Directories.Remove(path);

        public void DeleteFileIfExists(FileInfoEntity file) => FilePaths.Remove(file);

        public List<FileInfoEntity> GetFiles(PathAndPattern target, int daysOld)
        {
            var r = new Regex(CleanUpService.WildCardToRegex(target.Pattern));

            var files = FilePaths
                //Match on the the age and path first
                .Where(x =>
                    (_dateTimeService.Now - x.CreationTime).TotalDays >= daysOld &&
                    x.DirectoryName.StartsWith(target.Path)) //To simulate a deep search, matching directory prefix segments
                //Then match the remainder on the pattern (regex)
                .Where(x => r.IsMatch(x.Name))
                .ToList();

            return files;
        }

        //I can't really emulate this without making the directory more complex than a string
        public List<string> GetEmptyDirectories(PathAndPattern target) => new List<string>();

        public bool PathContainsFiles(string path) => FilePaths.Any(x => x.DirectoryName == path);
    }
}
