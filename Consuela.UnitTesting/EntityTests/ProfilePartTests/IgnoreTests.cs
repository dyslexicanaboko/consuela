using Consuela.Entity.ProfileParts;
using NUnit.Framework;

namespace Consuela.UnitTesting.EntityTests.ProfilePartTests
{
    [TestFixture]
    internal class IgnoreTests
        : CompareTestBase<Ignore>
    {
        [Test]
        public override void Non_empty_objects_are_equal()
        {
            //Arrange
            var left = new Ignore();
            left.Files.Add("F");
            left.Directories.Add("D");

            var right = new Ignore();
            right.Files.Add("F");
            right.Directories.Add("D");

            //Act / Assert
            AssertAreEqual(left, right);
        }

        [Test]
        public override void Objects_are_not_equal()
        {
            //Arrange
            var left = new Ignore();
            left.Files.Add("F0");
            left.Directories.Add("D0");

            var right = new Ignore();
            right.Files.Add("F1");
            right.Directories.Add("D1");

            //Act / Assert
            AssertAreNotEqual(left, right);
        }
    }
}
