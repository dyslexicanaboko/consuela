using Consuela.Entity.ProfileParts;
using NUnit.Framework;

namespace Consuela.UnitTesting.EntityTests.ProfilePartTests
{
    [TestFixture]
    internal class AuditTests
        : CompareTestBase<Audit>
    {
        protected override Audit GetFilledObject()
        {
            //Arrange
            var obj = new Audit();
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

            var right = new Audit();
            right.RetentionDays = 2;
            right.Path = "P1";
            right.Disable = false;

            //Act / Assert
            AssertAreNotEqual(left, right);
        }
    }
}
