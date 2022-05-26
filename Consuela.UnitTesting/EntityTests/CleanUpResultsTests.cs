using Consuela.Entity;
using NUnit.Framework;
using System;

namespace Consuela.UnitTesting.EntityTests
{
    [TestFixture]
    internal class CleanUpResultsTests
        : CompareTestBase<CleanUpResults>
    {
        protected override CleanUpResults GetFilledObject()
        {
            //Arrange
            var obj = new CleanUpResults();
            obj.FileDeleteErrors.Add(new FileInfoEntity(), new Exception());
            obj.FilesDeleted.Add(new FileInfoEntity());
            obj.DirectoryDeleteErrors.Add("directory", new Exception());
            obj.DirectoriesDeleted.Add("directory");

            return obj;
        }

        [Test]
        public override void Objects_are_not_equal()
        {
            //Arrange
            var left = GetFilledObject();

            var right = new CleanUpResults();
            right.FileDeleteErrors.Add(new FileInfoEntity { DirectoryName = "D0" }, new Exception());
            right.FilesDeleted.Add(new FileInfoEntity { DirectoryName = "D0" });
            right.DirectoryDeleteErrors.Add("directory0", new Exception());
            right.DirectoriesDeleted.Add("directory0");

            //Act / Assert
            AssertAreNotEqual(left, right);
        }
    }
}
