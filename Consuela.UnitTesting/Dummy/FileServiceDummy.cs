using Consuela.Entity;
using Consuela.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Consuela.UnitTesting.Dummy
{
    public class FileServiceDummy
        : IFileService
    {
        public List<FileInfoEntity> FilePaths { get; set; } = new List<FileInfoEntity>();
        
        public List<string> Directories { get; set; } = new List<string>();

        public void DeleteDirectory(string path) => Directories.Remove(path);

        public void DeleteFile(FileInfoEntity file) => FilePaths.Remove(file);

        public List<FileInfoEntity> GetFiles(PathAndPattern target, int daysOld)
        {
            var r = new Regex(CleanUpService.WildCardToRegex(target.Pattern));

            var files = FilePaths
                //Match on the the age and path first
                .Where(x =>
                    (DateTime.Now - x.CreationTime).TotalDays >= daysOld &&
                    x.DirectoryName.StartsWith(target.Path)) //To simulate a deep search, matching directory prefix segments
                //Then match the remainder on the pattern (regex)
                .Where(x => r.IsMatch(x.Name))
                .ToList();

            return files;
        }

        public bool PathContainsFiles(string path) => FilePaths.Any(x => x.DirectoryName == path);
    }
}
