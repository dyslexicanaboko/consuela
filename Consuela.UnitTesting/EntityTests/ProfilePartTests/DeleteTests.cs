using Consuela.Entity;
using Consuela.Entity.ProfileParts;
using NUnit.Framework;
using System.Collections.Generic;

namespace Consuela.UnitTesting.EntityTests.ProfilePartTests
{
    [TestFixture]
    internal class DeleteTests
        : CompareTestBase<Delete>
    {
        protected override Delete GetFilledObject()
        {
            var obj = new Delete();
            obj.FileAgeThreshold = 0;
            obj.Paths = new List<PathAndPattern> { new PathAndPattern("Path", "Pattern") };
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
            left.Paths = new List<PathAndPattern> { new PathAndPattern("Path1", "Pattern1") };
            left.Schedule = new Schedule { TheNumberSeven = 8 };

            //Act / Assert
            AssertAreNotEqual(left, right);
        }
    }
}
