using Consuela.Entity;
using Consuela.Lib.Services.ProfileManagement;
using Consuela.UnitTesting.Dummy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Consuela.UnitTesting
{
	public class ConsuelaTestBase
		: TestBase
	{
		protected const int ExpectedRemainingDirectoryCount = 2;
		protected const string BaseDrive = "D:";
		protected const string BaseDirectory = @"D:\Dump"; //Leave off trailing backslash for comparison purposes
		protected readonly DateTime ThirtyOneDaysAgo;

		protected ConsuelaTestBase()
			: base()
		{
			ThirtyOneDaysAgo = DateTime.Today.AddDays(-31);
		}

		protected ProfileManager LoadDefaultProfileManager()
		{
			var profileManager = new ProfileManager();
			profileManager.Profile = new ProfileWatcher();

			var profileSaver = new ProfileSaver();
			profileSaver.SetDefaultsAsNeeded(profileManager.Profile);

			return profileManager;
		}

		protected IProfile GetDefaultProfile() => LoadDefaultProfileManager().Profile;

		protected FileServiceDummy AddFiles(FileServiceDummy fileSystem, string path, int files)
		{
			if(fileSystem == null) throw new ArgumentNullException(nameof(fileSystem));

			var f = fileSystem;

			//Base directory needs to exist
			AddDirectory(f, path);

			var p = path == null ? BaseDirectory : path;

			//Using cardinal numbers
			for (var i = 1; i <= files; i++)
			{
				var file = Path.Combine(p, $"File{i:00}.txt");

				f.FilePaths.Add(new FileInfoEntity(file, ThirtyOneDaysAgo));
			}

			return f;
		}

		protected FileServiceDummy AddDirectory(FileServiceDummy fileSystem, string path)
		{
			if (fileSystem == null) throw new ArgumentNullException(nameof(fileSystem));

			if (path == null) return fileSystem;

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

			return fileSystem;
		}

		protected CleanUpResults GetExpectedResults(FileServiceDummy fileSystem)
		{
			var expected = new CleanUpResults();
			
			expected.FilesDeleted.AddRange(fileSystem.FilePaths.Select(x => x.Clone()));
			
			//Don't include the base drive or the base folder in the expected results as those should never be deleted
			expected.DirectoriesDeleted.AddRange(fileSystem.Directories.Where(x => x != BaseDrive && x != BaseDirectory));

			return expected;
		}

		protected List<FileInfoEntity> PathToFileInfoEntity(List<string> paths)
		{
			var lst = paths.Select(x => new FileInfoEntity(x, ThirtyOneDaysAgo)).ToList();

			return lst;
		}
	}
}
