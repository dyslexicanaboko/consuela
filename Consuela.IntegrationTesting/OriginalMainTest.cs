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

		[Test]
		public void Original_LinqPad_main_method()
		{
			var profileSaver = new ProfileSaver();
			var p = profileSaver.Get();

			var dtm = new DateTimeService();

			var fs = new FileService(dtm);

			var audit = new AuditService(p, fs, dtm);

			var svc = new CleanUpService(audit, fs);

			p.Delete.AddPath(new PathAndPattern(@"J:\Downloads\", "*"));
			p.Delete.AddPath(new PathAndPattern(@"J:\Dump\", "*"));

			p.Ignore.AddFile(CleanUpService.WildCardToRegex(@"temp*.*"));
			p.Ignore.AddDirectory(@"J:\Dump\Don't delete\");
			p.Ignore.AddDirectory(@"J:\Dump\Scan dump\");

			svc.CleanUp(p, DryRun);

			audit.SaveLog();
		}
	}
}
