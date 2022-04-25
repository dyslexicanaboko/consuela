using Consuela.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Consuela.Lib.Services
{
    public class CleanUpService : ICleanUpService
    {
        private readonly ILoggingService _loggingService;
        private readonly IFileService _fileService;

        public CleanUpService(
            ILoggingService loggingService,
            IFileService fileService)
        {
            _loggingService = loggingService;

            _fileService = fileService;
        }

        //Might want to consider returning statistics of some kind
        public void CleanUp(IProfile profile, bool dryRun)
        {
            var p = profile;

            var lstFiles = new List<FileInfoEntity>();

            //Get files to delete
            foreach (var obj in p.Delete.Paths)
            {
                lstFiles.AddRange(_fileService.GetFiles(obj, profile.Delete.FileAgeThreshold));
            }

            //FullName has to be used instead of DirectoryName because DirectoryName will throw an exception for anything over 260 characters
            p.Ignore.Directories.ForEach(d => lstFiles.RemoveAll(x => x.FullName.StartsWith(d)));

            //Remove all whitelisted files
            for (int i = lstFiles.Count - 1; i >= 0; i--)
            {
                var f = lstFiles[i].Name;

                if (SkipFile(f, p.Ignore.Files))
                    lstFiles.RemoveAt(i);
            }

            //Order by creation time
            lstFiles = lstFiles.OrderBy(x => x.CreationTime).ToList();

            _loggingService.Log($"Deleted Files {lstFiles.Count}");

            //Delete the individual files
            foreach (var f in lstFiles)
            {
                try
                {
                    _loggingService.Log(f);

                    if (!dryRun) _fileService.DeleteFile(f);
                }
                catch (Exception ex)
                {
                    //Oh well
                    _loggingService.Log(ex);
                }
            }

            var lstFolders = FindEmptyFoldersToDelete(lstFiles, p.Delete.Paths);

            _loggingService.Log($"Deleted Directories {lstFolders.Count}");

            //Lastly delete the folders that are empty, but not the search paths
            foreach (var folder in lstFolders)
            {
                try
                {
                    _loggingService.Log($"Directory,NULL,{folder},NULL");

                    if (!dryRun) _fileService.DeleteDirectory(folder);
                }
                catch (Exception ex)
                {
                    //Oh well
                    _loggingService.Log(ex);
                }
            }
        }

        //Get the folders to delete ultimately that are empty after files have been deleted
        private List<string> FindEmptyFoldersToDelete(List<FileInfoEntity> files, List<PathAndPattern> searchPaths)
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

                    var path = Path.GetDirectoryName(filePath);

                    //Remove all white listed paths
                    if (searchPaths.Any(p => p.Path == path)) continue;

                    //Make the list distinct manually
                    if (!paths.Contains(path))
                        paths.Add(path);
                }
                catch (PathTooLongException ptle)
                {
                    //Just leave these files behind because of this exception
                    _loggingService.Log(ptle.Message);
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
                    _loggingService.Log(ptle.Message);
                }
            }

            return paths;
        }

        private bool SkipFile(string value, IList<string> whiteList)
        {
            foreach (string r in whiteList)
            {
                if (Regex.IsMatch(value, r, RegexOptions.IgnoreCase))
                    return true;
            }

            return false;
        }

        public string WildCardToRegex(string value)
        {
            return "^" + Regex.Escape(value).Replace("\\*", ".*") + "$";
        }
    }
}
