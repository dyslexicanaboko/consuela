using Consuela.Entity;
using Consuela.Lib.Services.ProfileManagement;

namespace Consuela.UnitTesting
{
	public class ConsuelaTestBase
		: TestBase
	{
		protected ProfileManager LoadDefaultProfileManager()
		{
			var profileManager = new ProfileManager();
			profileManager.Profile = new ProfileWatcher();

			var profileSaver = new ProfileSaver();
			profileSaver.SetDefaultsAsNeeded(profileManager.Profile);

			return profileManager;
		}

		protected IProfile GetDefaultProfile() => LoadDefaultProfileManager().Profile;
	}
}
