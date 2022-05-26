using Consuela.Entity;
using Consuela.IntegrationTesting.FileCreation;
using NUnit.Framework;

namespace Consuela.IntegrationTesting
{
	[TestFixture]
	public class ScratchspaceTest
		: ConsuelaIntegrationTestBase
	{
		const bool DryRun = false;

		[Test]
		public void Ideal_test()
		{
			//Arrange
			var (p, svc) = GetCleanUpService();

			var files = new TestFileGenerationService();
			files.CreateDummyFiles(5, 1024, EntitySpacesCodeGen, "temp", ".tmp");
			files.CreateDummyFiles(5, 1024, EntitySpacesCodeGen, "File", ".txt");

			var fDeleted = files.CreateDummyFiles(5, 1024, BaseDirectory, "File", ".txt");

			var fIgnored = files.CreateDummyFiles(5, 1024, BaseDirectory, "temp", ".tmp");

			var dDeleted = files.CreateDummyFolders(5, BaseDirectory);

			var expected = new CleanUpResults
			{
				FilesDeleted = fDeleted,
				FilesIgnored = fIgnored,
				DirectoriesDeleted = dDeleted
			};

			//Act
			var actual = svc.CleanUp(p, DryRun);

			//Assert
			AssertAreEqual(expected, actual);
		}
	}
}
