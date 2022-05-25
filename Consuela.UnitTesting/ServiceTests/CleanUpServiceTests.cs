using Consuela.Entity;
using Consuela.Lib.Services;
using Consuela.Lib.Services.Dummy;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Consuela.UnitTesting.ServiceTests
{
    [TestFixture]
    public class CleanUpServiceTests
        : ConsuelaTestBase
    {
        private readonly Mock<IAuditService> _mockLoggingService = new Mock<IAuditService>();
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
            _mockFileService.Setup(x => x.DeleteFileIfExists(It.IsAny<FileInfoEntity>()));
            _mockFileService.Setup(x => x.DeleteDirectoryIfExists(It.IsAny<string>()));
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
            profile.Delete.AddPath(new PathAndPattern(BaseDirectory, "*"));

            var fileSystem = new FileServiceDummy();

            AddFiles(fileSystem, BaseDirectory, 10);

            var expected = GetSimpleExpectedResults(fileSystem);

            var svc = GetCleanUpService(fileSystem);

            //Act
            var actual = svc.CleanUp(profile, false);

            //Assert
            AssertAreEqual(expected, actual);
            Assert.AreEqual(ExpectedRemainingDirectoryCount, fileSystem.Directories.Count);
        }

        [Test]
        public void Non_empty_folders_are_not_deleted()
        {
            //Arrange
            var profile = GetDefaultProfile();
            profile.Delete.AddPath(new PathAndPattern(BaseDirectory, "File1*.txt"));

            //First 9 files won't have a preceding 1
            //Last 10 files will have a preceding 1
            var fileSystem = new FileServiceDummy();

            AddFiles(fileSystem, BaseDirectory, 19);

            var expected = GetSimpleExpectedResults(fileSystem);
            //In this instance overwrite the expected files to be deleted since the wild card is not matching preceding zeros
            expected.FilesDeleted = fileSystem.FilePaths.TakeLast(10).Select(x => x.Clone()).ToList();

            var svc = GetCleanUpService(fileSystem);

            //Act
            var actual = svc.CleanUp(profile, false);

            //Assert
            AssertAreEqual(expected, actual);
            Assert.AreEqual(ExpectedRemainingDirectoryCount, fileSystem.Directories.Count);
        }

        [Test]
        public void Empty_folders_are_deleted()
        {
            //Arrange
            var innerDirectory = Path.Combine(BaseDirectory, "Dir1");

            var profile = GetDefaultProfile();
            profile.Delete.AddPath(new PathAndPattern(BaseDirectory, "File*.txt"));

            var fileSystem = new FileServiceDummy();

            AddFiles(fileSystem, innerDirectory, 5);

            var expected = GetSimpleExpectedResults(fileSystem);

            var svc = GetCleanUpService(fileSystem);

            //Act
            var actual = svc.CleanUp(profile, false);

            //Assert
            AssertAreEqual(expected, actual);
            Assert.AreEqual(ExpectedRemainingDirectoryCount, fileSystem.Directories.Count);
        }

        [Test]
        public void Ideal_test() //I don't have a good name for this right now
        {
            //Arrange
            var ignoreDirectory = Path.Combine(BaseDirectory, "EntitySpacesCodeGen");

            var profile = GetDefaultProfile();
            profile.Ignore.AddFile("temp*");
            profile.Ignore.AddDirectory(ignoreDirectory);
            profile.Delete.AddPath(new PathAndPattern(BaseDirectory, "*"));

            var fileSystem = new FileServiceDummy();

            var fDeleted = AddFiles(fileSystem, BaseDirectory, 5);

            var fIgnored = AddFiles(fileSystem, BaseDirectory, 5, "temp{0}.tmp");
            
            AddFiles(fileSystem, ignoreDirectory, 5);

            var expected = new CleanUpResults
            {
                FilesDeleted = fDeleted,
                FilesIgnored = fIgnored
            };

            var svc = GetCleanUpService(fileSystem);

            //Act
            var actual = svc.CleanUp(profile, false);

            //Assert
            AssertAreEqual(expected, actual);
            Assert.AreEqual(1, DirectoriesWithoutBase(fileSystem).Count());
        }
    }
}
