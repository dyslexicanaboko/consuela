using Consuela.Lib.Services.ProfileManagement;
using Moq;
using NUnit.Framework;

namespace Consuela.UnitTesting.ServiceTests
{
	[TestFixture]
	public class ProfileManagementTests
		: TestBase
	{
		private readonly Mock<IProfileSaver> _profileSaverMock = new Mock<IProfileSaver>();

		public ProfileManagementTests()
		{
			_profileSaverMock.Setup(x => x.Load()).Returns(LoadDefaultProfile());
			_profileSaverMock.Setup(s => s.Save());
		}

		private ProfileManager LoadDefaultProfile()
		{
			var profileManager = new ProfileManager();
			profileManager.Profile = new ProfileWatcher();

			var profileSaver = new ProfileSaver();
			profileSaver.SetDefaultsAsNeeded(profileManager.Profile);

			return profileManager;
		}

		[Test]
		public void Empty_config_loads_defaults()
		{
			//Arrange
			var profileSaver = _profileSaverMock.Object;

			var expected = new ProfileWatcher();
			expected.Delete.FileAgeThreshold = 30;
			expected.Logging.RetentionDays = 30;
			expected.Logging.Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

			//Act
			var actual = profileSaver.Load().Profile;

			//Assert
			AssertAreEqual(expected, actual);
		}
	}
}
