using Consuela.Entity;
using Consuela.Lib.Services.ProfileManagement;
using Consuela.UnitTesting.Dummy;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace Consuela.UnitTesting
{
	public class ConsuelaTestBase
		: TestBase
	{
		protected const string SomeDirectory = @"D:\Dump\";
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

		protected FileServiceDummy GetFileSystem(int files)
		{
			var f = new FileServiceDummy();
			f.Directories.Add(SomeDirectory);

			//Using cardinal numbers
			for (var i = 1; i <= files; i++)
			{
				var file = Path.Combine(SomeDirectory, $"File{i:00}.txt");

				f.FilePaths.Add(new FileInfoEntity(file, ThirtyOneDaysAgo));
			}

			return f;
		}

		protected List<FileInfoEntity> PathToFileInfoEntity(List<string> paths)
		{
			var lst = paths.Select(x => new FileInfoEntity(x, ThirtyOneDaysAgo)).ToList();

			return lst;
		}
	}
}
