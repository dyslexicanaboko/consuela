using Consuela.Entity;
using Consuela.Lib.Services;
using Consuela.Lib.Services.Dummy;
using Consuela.Lib.Services.ProfileManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Consuela.UnitTesting
{
	public class ConsuelaTestBase
		: TestBase
	{
		protected virtual string BaseDrive { get; set; } = "D:";
		//Leave off trailing backslash for comparison purposes
		protected virtual string BaseDirectory { get; set; } = @"D:\Dump";

		protected const int ExpectedRemainingDirectoryCount = 2;
		protected readonly DateTime ThirtyFiveDaysAhead;
		protected readonly DateTime ThirtyFiveDaysBehind;

		protected ConsuelaTestBase()
			: base()
		{
			ThirtyFiveDaysAhead = DateTime.Today.AddDays(35);

			ThirtyFiveDaysBehind = DateTime.Today.AddDays(-35);
		}
		
		protected FileServiceDummy FileService() => new FileServiceDummy(new DateTimeService());

		protected ProfileManager LoadDefaultProfileManager()
		{
			var profileManager = new ProfileManager();
			profileManager.Profile = new ProfileWatcher();

			var profileSaver = new ProfileSaver();
			profileSaver.SetDefaultsAsNeeded(profileManager.Profile);

			return profileManager;
		}

		protected IProfile GetDefaultProfile() => LoadDefaultProfileManager().Profile;

		protected List<FileInfoEntity> AddFiles(FileServiceDummy fileSystem, string path, int files, string pattern = "File{0}.txt")
		{
			if(fileSystem == null) throw new ArgumentNullException(nameof(fileSystem));

			var added = new List<FileInfoEntity>(files);

			var f = fileSystem;

			//Base directory needs to exist
			AddDirectory(f, path);

			var p = path == null ? BaseDirectory : path;

			//Using cardinal numbers
			for (var i = 1; i <= files; i++)
			{
				var file = Path.Combine(p, string.Format(pattern, $"{i:00}"));

				var fie = new FileInfoEntity(file, ThirtyFiveDaysBehind);

				added.Add(fie);

				f.FilePaths.Add(fie);
			}

			return added;
		}

		protected void AddDirectory(FileServiceDummy fileSystem, string path)
		{
			if (fileSystem == null) throw new ArgumentNullException(nameof(fileSystem));

			if (path == null) return;

			//Break down the path into parts
			var pathParts = path.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);

			for (var i = 0; i < pathParts.Length; i++)
			{
				var l = i + 1;

				//Build a new path array for this segment of path
				var arr = new string[l];

				//Copy the elements to a new array so just what is needed is included in the combine
				Array.ConstrainedCopy(pathParts, 0, arr, 0, l);

				//Combine the directories together to form a new path segment
				var pathSegment = Path.Combine(arr);

				//If this path segment exists already skip
				if (fileSystem.Directories.Contains(pathSegment)) continue;

				//If this path segment does not exist then add
				fileSystem.Directories.Add(pathSegment);
			}
		}

		/// <summary>
		/// This can be used when nothing is being ignored. This won't help if things are supposed to be ignored.
		/// </summary>
		/// <param name="fileSystem">Existing file system to mimic</param>
		/// <returns></returns>
		protected CleanUpResults GetSimpleExpectedResults(FileServiceDummy fileSystem)
		{
			var expected = new CleanUpResults();
			
			expected.FilesDeleted.AddRange(fileSystem.FilePaths.Select(x => x.Clone()));
			
			//Don't include the base drive or the base folder in the expected results as those should never be deleted
			expected.DirectoriesDeleted.AddRange(DirectoriesWithoutBase(fileSystem));

			return expected;
		}

		protected IEnumerable<string> DirectoriesWithoutBase(FileServiceDummy fileSystem) 
			=> fileSystem.Directories.Where(x => x != BaseDrive && x != BaseDirectory);

		protected List<FileInfoEntity> PathToFileInfoEntity(List<string> paths)
		{
			var lst = paths.Select(x => new FileInfoEntity(x, ThirtyFiveDaysBehind)).ToList();

			return lst;
		}
	}
}
