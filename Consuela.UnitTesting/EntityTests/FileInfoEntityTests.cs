using Consuela.Entity;
using NUnit.Framework;

namespace Consuela.UnitTesting.EntityTests
{
    [TestFixture]
    internal class FileInfoEntityTests
        : CompareTestBase<FileInfoEntity>
    {
        protected override FileInfoEntity GetFilledObject()
        {
            //Arrange
            var obj = new FileInfoEntity();
            obj.FullName = "FN0";
            obj.DirectoryName = "DN0";
            obj.Name = "N0";

            return obj;
        }

        [Test]
        public override void Objects_are_not_equal()
        {
            //Arrange
            var left = GetFilledObject();

            var right = new FileInfoEntity();
            right.FullName = "FN1";
            right.DirectoryName = "DN1";
            right.Name = "N1";

            //Act / Assert
            AssertAreNotEqual(left, right);
        }
    }
}
