using Consuela.Entity.ProfileParts;
using NUnit.Framework;
using System.Linq;

namespace Consuela.UnitTesting.EntityTests.ProfilePartTests
{
    [TestFixture]
    internal class IgnoreTests
        : CompareTestBase<Ignore>
    {
        protected override Ignore GetFilledObject()
        {
            //Arrange
            var obj = new Ignore();
            obj.AddFile("F");
            obj.AddDirectory("D");

            return obj;
        }

        [Test]
        public override void Objects_are_not_equal()
        {
            //Arrange
            var left = GetFilledObject();

            var right = new Ignore();
            right.AddFile("F1");
            right.AddDirectory("D1");

            //Act / Assert
            AssertAreNotEqual(left, right);
        }

        [Test]
        public void File_list_must_be_distinct()
        {
            //Arrange
            var obj = new Ignore();
            obj.AddFile("F");
            obj.AddFile("F");

            //Act
            //Count how many times this object occurs
            var actual = obj.Files.Count(x => x == "F");

            //Assert
            //Expecting the object to show up once
            Assert.AreEqual(1, actual);
        }

        [Test]
        public void Directory_list_must_be_distinct()
        {
            //Arrange
            var obj = new Ignore();
            obj.AddDirectory("D");
            obj.AddDirectory("D");

            //Act
            //Count how many times this object occurs
            var actual = obj.Directories.Count(x => x == "D");

            //Assert
            //Expecting the object to show up once
            Assert.AreEqual(1, actual);
        }
    }
}
