using Consuela.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Consuela.Lib.Services
{
    public class CleanUpService 
        : ICleanUpService
    {
        private readonly IAuditService _auditService;
        private readonly IFileService _fileService;

        public CleanUpService(
            IAuditService auditService,
            IFileService fileService)
        {
            _auditService = auditService;

            _fileService = fileService;
        }

        public CleanUpResults CleanUp(IProfile profile, bool dryRun)
        {
            //Prepare for a new run by resetting all stateful variables
            _auditService.Reset();

            var results = new CleanUpResults();

            //Get files to delete per target path
            foreach (var target in profile.Delete.Paths)
            {
                ProcessDeletePath(profile, target, results, dryRun);
            }

            _auditService.SaveLog();

            _auditService.SaveStatistics(results);

            return results;
        }

        //Process one target at a time so that each one is processed individually according to its own rules
        //This will avoid path conflicts between ignore and delete paths
        private void ProcessDeletePath(IProfile profile, PathAndPattern target, CleanUpResults results, bool dryRun)
        {
            var lstFiles = _fileService.GetFiles(target, profile.Delete.FileAgeThreshold);

            //FullName has to be used instead of DirectoryName because DirectoryName will throw an exception for anything over 260 characters
            foreach (var d in profile.Ignore.Directories)
            {
                //Only remove the ignored directories if it is NOT the target
                //If it is the target then it means it needs to be cleaned (nested folder that is also a target path)
                if (d == target.Path) continue;

                //Remove all ignored [directories]
                lstFiles.RemoveAll(x => x.FullName.StartsWith(d));
            }

            //Remove all ignored [files]
            for (int i = lstFiles.Count - 1; i >= 0; i--)
            {
                var f = lstFiles[i];

                if (SkipFile(f.Name, profile.Ignore.Files))
                {
                    results.FilesIgnored.Add(f);

                    lstFiles.RemoveAt(i);
                }
            }

            //Order by creation time
            lstFiles = lstFiles.OrderBy(x => x.CreationTime).ToList();

            Console.WriteLine($"Deleted Files {lstFiles.Count}");

            //Delete the individual files
            foreach (var f in lstFiles)
            {
                try
                {
                    _auditService.LogFile(f);

                    if (!dryRun) _fileService.DeleteFileIfExists(f);

                    results.FilesDeleted.Add(f);
                }
                catch (Exception ex)
                {
                    results.FileDeleteErrors.TryAdd(f, ex);
                }
            }

            var lstFolders = FindEmptyFoldersToDelete(results, lstFiles, new[] { target });

            Console.WriteLine($"Deleted Directories {lstFolders.Count}");

            //Lastly delete the folders that are empty, but not the search paths
            foreach (var folder in lstFolders)
            {
                try
                {
                    _auditService.LogDirectory($"Directory,NULL,{folder},NULL");

                    if (!dryRun) _fileService.DeleteDirectoryIfExists(folder);

                    results.DirectoriesDeleted.Add(folder);
                }
                catch (Exception ex)
                {
                    results.DirectoryDeleteErrors.TryAdd(folder, ex);
                }
            }
        }

        //Get the folders to delete ultimately that are empty after files have been deleted
        private List<string> FindEmptyFoldersToDelete(CleanUpResults results, List<FileInfoEntity> files, IReadOnlyList<PathAndPattern> searchPaths)
        {
            //Unfortunately because of the PathTooLongException I have to jump through hoops to make this work
            List<string> lst =
                files
                    .Select(x => x.FullName)
                    .Distinct()
                    .ToList();

            //Get distinct paths manually
            var paths = new List<string>();

            for (var i = lst.Count - 1; i > 0; i--)
            {
                var filePath = lst[i];

                try
                {
                    //This call can thrown an exception, so it has to be done one at a time
                    var path = Path.GetDirectoryName(filePath);

                    //Remove all ignored paths from the ignore list
                    if (searchPaths.Any(p => p.Path == path)) continue;

                    //Make the list distinct manually
                    if (!paths.Contains(path))
                        paths.Add(path);
                }
                catch (PathTooLongException ptle)
                {
                    //Just leave these directories behind because of this exception
                    //As a work around using the filePath since I cannot capture the directory only
                    results.DirectoryDeleteErrors.TryAdd(filePath, ptle);
                }
            }

            //Remove all directories that aren't empty
            for (var i = paths.Count - 1; i > 0; i--)
            {
                var path = paths[i];

                try
                {
                    if (_fileService.PathContainsFiles(path))
                    {
                        lst.RemoveAt(i);
                    }
                }
                catch (PathTooLongException ptle)
                {
                    //Just leave these directories behind because of this exception
                    results.DirectoryDeleteErrors.TryAdd(path, ptle);
                }
            }

            //Add all stand alone empty folders that were empty to begin with
            foreach (var search in searchPaths)
            {
                //Since files are the main driver, folders that were empty to begin with will never be deleted unless
                //they are specifically looked for to be deleted
                paths.AddRange(_fileService.GetEmptyDirectories(search));
            }

            return paths;
        }

        private bool SkipFile(string value, IReadOnlyList<string> ignoreList)
        {
            foreach (string file in ignoreList)
            {
                //If the ignore list item contains a wild card then convert it to proper regex
                var search = file.Contains("*") ? WildCardToRegex(file) : file;

                //This will search both regex and non-regex just fine from my experience so far
                if (Regex.IsMatch(value, search, RegexOptions.IgnoreCase))
                    return true;
            }

            return false;
        }

        public static string WildCardToRegex(string value)
            => "^" + Regex.Escape(value).Replace("\\*", ".*") + "$";
    }
}
