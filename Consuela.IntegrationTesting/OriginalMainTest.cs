using Consuela.Lib;
using Consuela.Lib.Services;
using NUnit.Framework;
using System.IO;

namespace Consuela.IntegrationTesting
{
	[TestFixture]
	public class OriginalMainTest
	{
		const bool DryRun = true;
		private string BasePath = Path.GetDirectoryName(Util_CurrentQueryPath());

		public void Original_LinqPad_main_method()
		{
			var svc = new CleanUpService();

			var p = new Profile();

			p.SearchPaths.Add(new PathAndPattern(@"J:\Downloads\", "*"));
			p.SearchPaths.Add(new PathAndPattern(@"J:\Dump\", "*"));

			p.WhiteListFiles.Add(svc.WildCardToRegex(@"temp*.*"));
			p.WhiteListDirectories.Add(@"J:\Dump\Don't delete\");
			p.WhiteListDirectories.Add(@"J:\Dump\Scan dump\");

			var operations = svc.CleanUp(p, DryRun);

			var path = Path.Combine(BasePath, "Delete operations.log");

			var txt = svc.GetText(operations);

			File.AppendAllText(path, txt);
		}

		//LinqPad's Util.CurrentQueryPath loose equivalent
		private static string Util_CurrentQueryPath()
		{
			var path = System.Reflection.Assembly.GetExecutingAssembly().Location;

			return path;
		}
	}
}
