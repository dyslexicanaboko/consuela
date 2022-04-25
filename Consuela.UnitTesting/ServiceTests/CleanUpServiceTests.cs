using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Consuela.Lib.Services;

namespace Consuela.UnitTesting.ServiceTests
{
    [TestFixture]
    public class CleanUpServiceTests
    {
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
    }
}
