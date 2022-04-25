using Consuela.Entity;
using Consuela.Lib.Services;
using Consuela.Lib.Services.ProfileManagement;
using NUnit.Framework;

namespace Consuela.IntegrationTesting
{
	[TestFixture]
	public class OriginalMainTest
	{
		const bool DryRun = true;

		public void Original_LinqPad_main_method()
		{
			var p = new ProfileWatcher();

			var logging = new LoggingService(p);

			var svc = new CleanUpService(
				logging,
				new FileService());

			p.Delete.Paths.Add(new PathAndPattern(@"J:\Downloads\", "*"));
			p.Delete.Paths.Add(new PathAndPattern(@"J:\Dump\", "*"));

			p.Ignore.Files.Add(svc.WildCardToRegex(@"temp*.*"));
			p.Ignore.Directories.Add(@"J:\Dump\Don't delete\");
			p.Ignore.Directories.Add(@"J:\Dump\Scan dump\");

			svc.CleanUp(p, DryRun);
		}
	}
}
