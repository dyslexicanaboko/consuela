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
			var profileSaver = new ProfileSaver();
			var profileManager = profileSaver.Load();
			var p = profileManager.Profile;

			var logging = new LoggingService(p);

			var svc = new CleanUpService(
				logging,
				new FileService());

			p.Delete.AddPath(new PathAndPattern(@"J:\Downloads\", "*"));
			p.Delete.AddPath(new PathAndPattern(@"J:\Dump\", "*"));

			p.Ignore.AddFile(CleanUpService.WildCardToRegex(@"temp*.*"));
			p.Ignore.AddDirectory(@"J:\Dump\Don't delete\");
			p.Ignore.AddDirectory(@"J:\Dump\Scan dump\");

			svc.CleanUp(p, DryRun);

			logging.SaveLog();
		}
	}
}
