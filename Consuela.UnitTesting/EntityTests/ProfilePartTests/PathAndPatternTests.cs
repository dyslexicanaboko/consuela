using Consuela.Entity;
using NUnit.Framework;

namespace Consuela.UnitTesting.EntityTests.ProfilePartTests
{
    [TestFixture]
    internal class PathAndPatternTests
        : CompareTestBase<PathAndPattern>
    {
        protected override PathAndPattern GetFilledObject()
        {
            //Arrange
            var obj = new PathAndPattern("Path", "Pattern");

            return obj;
        }

        [Test]
        public override void Objects_are_not_equal()
        {
            //Arrange
            var left = GetFilledObject();

            var right = new PathAndPattern("Path1", "Pattern1");

            //Act / Assert
            AssertAreNotEqual(left, right);
        }
    }
}
