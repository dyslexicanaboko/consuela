using Consuela.Entity;
using Consuela.Lib.Services;
using Consuela.Lib.Services.ProfileManagement;
using NUnit.Framework;
using System.IO;

namespace Consuela.UnitTesting.ServiceTests
{
	[TestFixture]
	public class ConfigurationTests
	{
		[Test]
		public void Empty_config_loads_defaults()
		{
			//Arrange
			var profileSaver = new ProfileSaver();


			//Act
			var profileManager = profileSaver.Load();

			//Assert
			//Compare each part of the profile

		}

		private void AssertAreEqual(IProfile expected, IProfile actual)
		{
			//Compare ignores


			//Compare logging


			//Compare deletes

		}
	}
}
