using Consuela.Entity;
using Consuela.Lib.Services;
using Consuela.Lib.Services.ProfileManagement;
using NUnit.Framework;
using System.IO;

namespace Consuela.IntegrationTesting
{
	[TestFixture]
	public class OriginalMainTest
	{
		const bool DryRun = true;

		public void Original_LinqPad_main_method()
		{
			var svc = new CleanUpService();

			var p = new ProfileWatcher();

			p.Delete.Paths.Add(new PathAndPattern(@"J:\Downloads\", "*"));
			p.Delete.Paths.Add(new PathAndPattern(@"J:\Dump\", "*"));

			p.Ignore.Files.Add(svc.WildCardToRegex(@"temp*.*"));
			p.Ignore.Directories.Add(@"J:\Dump\Don't delete\");
			p.Ignore.Directories.Add(@"J:\Dump\Scan dump\");

			var operations = svc.CleanUp(p, DryRun);

			var path = Path.Combine(p.Logging.Path, "Delete operations.log");

			var txt = svc.GetText(operations);

			File.AppendAllText(path, txt);
		}
	}
}
