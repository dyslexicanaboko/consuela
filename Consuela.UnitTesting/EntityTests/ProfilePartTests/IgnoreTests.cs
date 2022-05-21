using Consuela.Entity.ProfileParts;
using NUnit.Framework;

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
    }
}
