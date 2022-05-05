using Consuela.Entity;
using Consuela.Lib.Services;
using Moq;
using NUnit.Framework;

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
            //I am thinking of having a dummy file system where fake files can be deleted
            //Tests:
            //Zero files deleted with empty profile
            //10 files deleted, no directories
            //Ignored files are no deleted
            //Directories with ignored files will not be deleted
            //
            //_mockFileService.Setup(x => x.)
        }

        private CleanUpService GetService()
        {
            _mockLoggingService.Setup(m => m.Log(It.IsAny<string>()));
            _mockFileService.Setup(x => x.DeleteFile(It.IsAny<FileInfoEntity>()));
            _mockFileService.Setup(x => x.DeleteDirectory(It.IsAny<string>()));
            _mockFileService.Setup(x => x.GetFiles(It.IsAny<PathAndPattern>(), It.IsAny<int>()));
            _mockFileService.Setup(x => x.PathContainsFiles(It.IsAny<string>())).Returns(true);

            var svc = new CleanUpService(
                _mockLoggingService.Object,
                _mockFileService.Object);

            return svc;
        }

        [Test]
        public void Empty_profile_deletes_zero_files()
        {
            //Arrange
            var svc = GetService();
            var profile = GetDefaultProfile();
            var expected = new CleanUpResults();

            //Act
            var actual = svc.CleanUp(profile, false);

            //Assert
            AssertAreEqual(expected, actual);
        }
    }
}
