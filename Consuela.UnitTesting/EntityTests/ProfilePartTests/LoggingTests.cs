using Consuela.Entity.ProfileParts;
using NUnit.Framework;

namespace Consuela.UnitTesting.EntityTests.ProfilePartTests
{
    [TestFixture]
    internal class LoggingTests
        : CompareTestBase<Logging>
    {
        protected override Logging GetFilledObject()
        {
            //Arrange
            var obj = new Logging();
            obj.RetentionDays = 1;
            obj.Path = "P";
            obj.Disable = true;

            return obj;
        }

        [Test]
        public override void Objects_are_not_equal()
        {
            //Arrange
            var left = GetFilledObject();

            var right = new Logging();
            right.RetentionDays = 2;
            right.Path = "P1";
            right.Disable = false;

            //Act / Assert
            AssertAreNotEqual(left, right);
        }
    }
}
