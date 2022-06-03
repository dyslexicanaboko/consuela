using Consuela.Lib.Services.ProfileManagement;
using Moq;
using NUnit.Framework;

namespace Consuela.UnitTesting.ServiceTests
{
	[TestFixture]
	public class ProfileTests
		: ConsuelaTestBase
	{
		private readonly Mock<IProfileSaver> _profileSaverMock = new Mock<IProfileSaver>();

		public ProfileTests()
		{
			_profileSaverMock.Setup(x => x.Get()).Returns(GetDefaultProfile());
			_profileSaverMock.Setup(s => s.Save());
		}

		[Test]
		public void Empty_config_loads_defaults()
		{
			//Arrange
			var profileSaver = _profileSaverMock.Object;

			var expected = new Profile();
			expected.Delete.FileAgeThreshold = 30;
			expected.Audit.RetentionDays = 30;
			expected.Audit.Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

			//Act
			var actual = (Profile)profileSaver.Get();

			//Assert
			AssertAreEqual(expected, actual);
		}
	}
}
