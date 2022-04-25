using Consuela.Entity.ProfileParts;
using NUnit.Framework;

namespace Consuela.UnitTesting.EntityTests.ProfilePartTests
{
    [TestFixture]
    internal class IgnoreTests
    {
        [Test]
        public void Null_objects_are_equal()
        {
            //Arrange
            Ignore left = null;
            Ignore right = null;

            //Act
            var areEqual = left == right;

            //Assert
            Assert.IsTrue(areEqual);
        }

        [Test]
        public void Left_object_is_null_then_not_equal()
        {
            //Arrange
            Ignore left = null;
            var right = new Ignore();

            //Act
            var areEqual = left == right;

            //Assert
            Assert.IsFalse(areEqual);
        }

        [Test]
        public void Right_object_is_null_then_not_equal()
        {
            //Arrange
            var left = new Ignore();
            Ignore right = null;

            //Act
            var areEqual = left == right;

            //Assert
            Assert.IsFalse(areEqual);
        }

        [Test]
        public void Empty_objects_are_equal()
        {
            //Arrange
            var left = new Ignore();
            var right = new Ignore();

            //Act
            var areEqual = left == right;

            //Assert
            Assert.IsTrue(areEqual);
        }

        [Test]
        public void Non_empty_objects_are_equal()
        {
            //Arrange
            var left = new Ignore();
            left.Files.Add("F");
            left.Directories.Add("D");

            var right = new Ignore();
            right.Files.Add("F");
            right.Directories.Add("D");

            //Act
            var areEqual = left == right;

            //Assert
            Assert.IsTrue(areEqual);
        }

        [Test]
        public void Objects_are_not_equal()
        {
            //Arrange
            var left = new Ignore();
            left.Files.Add("F0");
            left.Directories.Add("D0");

            var right = new Ignore();
            right.Files.Add("F1");
            right.Directories.Add("D1");

            //Act
            var areEqual = left == right;

            //Assert
            Assert.IsFalse(areEqual);
        }
    }
}
