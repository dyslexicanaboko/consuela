using Consuela.Entity;
using Consuela.Lib.Services;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Consuela.UnitTesting.ServiceTests
{
    [TestFixture]
    public class CleanUpServiceTests
        : ConsuelaTestBase
    {
        private readonly Mock<ILoggingService> _mockLoggingService = new Mock<ILoggingService>();
        private readonly Mock<IFileService> _mockFileService = new Mock<IFileService>();
        
        public CleanUpServiceTests()
        {
            //Tests:
            //10 files deleted, no directories
            //Ignored files are not deleted
            //Directories with ignored files will not be deleted
        }

        private CleanUpService GetCleanUpService(IFileService fileService = null)
        {
            //This will never change for any of the tests
            _mockLoggingService.Setup(m => m.Log(It.IsAny<string>()));

            //These are the default setups
            _mockFileService.Setup(x => x.DeleteFile(It.IsAny<FileInfoEntity>()));
            _mockFileService.Setup(x => x.DeleteDirectory(It.IsAny<string>()));
            _mockFileService.Setup(x => x.GetFiles(It.IsAny<PathAndPattern>(), It.IsAny<int>()));
            _mockFileService.Setup(x => x.PathContainsFiles(It.IsAny<string>())).Returns(true);

            var fs = fileService == null ? _mockFileService.Object : fileService;

            var svc = new CleanUpService(
                _mockLoggingService.Object,
                fs);

            return svc;
        }

        [Test]
        public void Empty_profile_deletes_zero_files()
        {
            //Arrange
            var svc = GetCleanUpService();
            var profile = GetDefaultProfile();
            var expected = new CleanUpResults();

            //Act
            var actual = svc.CleanUp(profile, false);

            //Assert
            AssertAreEqual(expected, actual);
        }
        
        [Test]
        public void Delete_path_parent_folder_will_not_be_deleted()
        {
            //Arrange
            var profile = GetDefaultProfile();
            profile.Delete.Paths.Add(new PathAndPattern(SomeDirectory, "*"));

            var fileSystem = GetFileSystem(10);

            var expected = new CleanUpResults();
            expected.FilesDeleted.AddRange(fileSystem.FilePaths.Select(x => x.Clone()));

            var svc = GetCleanUpService(fileSystem);

            //Act
            var actual = svc.CleanUp(profile, false);

            //Assert
            AssertAreEqual(expected, actual);
            Assert.AreEqual(1, fileSystem.Directories.Count);
        }

        [Test]
        public void Non_empty_folders_are_not_deleted()
        {
            //Arrange
            var profile = GetDefaultProfile();
            profile.Delete.Paths.Add(new PathAndPattern(SomeDirectory, "File1*.txt"));

            //First 9 files won't have a preceding 1
            //Last 10 files will have a preceding 1
            var fileSystem = GetFileSystem(19);

            var expected = new CleanUpResults();
            expected.FilesDeleted.AddRange(fileSystem.FilePaths.TakeLast(10).Select(x => x.Clone()));

            var svc = GetCleanUpService(fileSystem);

            //Act
            var actual = svc.CleanUp(profile, false);

            //Assert
            AssertAreEqual(expected, actual);
            Assert.AreEqual(1, fileSystem.Directories.Count);
        }
    }
}
