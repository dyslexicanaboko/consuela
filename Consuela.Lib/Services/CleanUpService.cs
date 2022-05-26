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
            var results = new CleanUpResults();

            var lstFiles = new List<FileInfoEntity>();

            //Get files to delete
            foreach (var target in profile.Delete.Paths)
            {
                lstFiles.AddRange(_fileService.GetFiles(target, profile.Delete.FileAgeThreshold));
            }

            //FullName has to be used instead of DirectoryName because DirectoryName will throw an exception for anything over 260 characters
            foreach (var d in profile.Ignore.Directories)
            {
                lstFiles.RemoveAll(x => x.FullName.StartsWith(d));
            }

            //Remove all ignored files
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

            _auditService.Log($"Deleted Files {lstFiles.Count}");

            //Delete the individual files
            foreach (var f in lstFiles)
            {
                try
                {
                    _auditService.Log(f);

                    if (!dryRun) _fileService.DeleteFileIfExists(f);

                    results.FilesDeleted.Add(f);
                }
                catch (Exception ex)
                {
                    results.FileDeleteErrors.Add(f, ex);

                    //Oh well
                    _auditService.Log(ex);
                }
            }

            var lstFolders = FindEmptyFoldersToDelete(lstFiles, profile.Delete.Paths);

            _auditService.Log($"Deleted Directories {lstFolders.Count}");

            //Lastly delete the folders that are empty, but not the search paths
            foreach (var folder in lstFolders)
            {
                try
                {
                    _auditService.Log($"Directory,NULL,{folder},NULL");

                    if (!dryRun) _fileService.DeleteDirectoryIfExists(folder);

                    results.DirectoriesDeleted.Add(folder);
                }
                catch (Exception ex)
                {
                    results.DirectoryDeleteErrors.Add(folder, ex);
                    
                    //Oh well
                    _auditService.Log(ex);
                }
            }

            _auditService.SaveLog();

            return results;
        }

        //Get the folders to delete ultimately that are empty after files have been deleted
        private List<string> FindEmptyFoldersToDelete(List<FileInfoEntity> files, IReadOnlyList<PathAndPattern> searchPaths)
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
                try
                {
                    var filePath = lst[i];

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
                    //Just leave these files behind because of this exception
                    _auditService.Log(ptle.Message);
                }
            }

            //Remove all directories that aren't empty
            for (var i = paths.Count - 1; i > 0; i--)
            {
                try
                {
                    var path = paths[i];

                    if (_fileService.PathContainsFiles(path))
                    {
                        lst.RemoveAt(i);
                    }
                }
                catch (PathTooLongException ptle)
                {
                    //TODO: Needs to be logged properly
                    //Just leave these files behind because of this exception
                    _auditService.Log(ptle.Message);
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
