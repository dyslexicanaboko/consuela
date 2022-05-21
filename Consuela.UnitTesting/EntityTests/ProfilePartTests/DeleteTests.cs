using Consuela.Entity;
using Consuela.Entity.ProfileParts;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Consuela.UnitTesting.EntityTests.ProfilePartTests
{
    [TestFixture]
    internal class DeleteTests
        : CompareTestBase<Delete>
    {
        private readonly PathAndPattern SomePathAndPattern = new PathAndPattern("Path", "Pattern");

        protected override Delete GetFilledObject()
        {
            var obj = new Delete();
            obj.FileAgeThreshold = 0;
            obj.AddPath(SomePathAndPattern);
            obj.Schedule = new Schedule();

            return obj;
        }

        [Test]
        public override void Objects_are_not_equal()
        {
            //Arrange
            var left = GetFilledObject();

            var right = new Delete();
            left.FileAgeThreshold = 1;
            left.AddPath(new PathAndPattern("Path1", "Pattern1"));
            left.Schedule = new Schedule { TheNumberSeven = 8 };

            //Act / Assert
            AssertAreNotEqual(left, right);
        }

        [Test]
        public void Paths_list_must_be_distinct()
        {
            //Arrange
            var left = new Delete();
            left.AddPath(SomePathAndPattern);
            left.AddPath(SomePathAndPattern);

            //Act
            //Count how many times this object occurs
            var actual = left.Paths.Count(x => x == SomePathAndPattern);

            //Assert
            //Expecting the object to show up once
            Assert.AreEqual(1, actual);
        }
    }
}
