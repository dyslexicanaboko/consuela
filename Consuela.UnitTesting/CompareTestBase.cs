using NUnit.Framework;
using System;

namespace Consuela.UnitTesting
{
    internal abstract class CompareTestBase<T>
        where T : class, IEquatable<T>, new()
    {
        [Test]
        public virtual void Null_objects_are_equal()
        {
            //Arrange
            T left = null;
            T right = null;

            //Act / Assert
            AssertAreEqual(left, right);
        }

        [Test]
        public virtual void Left_object_is_null_then_not_equal()
        {
            //Arrange
            T left = null;
            var right = new T();

            //Act / Assert
            AssertAreNotEqual(left, right);
        }

        [Test]
        public virtual void Right_object_is_null_then_not_equal()
        {
            //Arrange
            var left = new T();
            T right = null;

            //Act / Assert
            AssertAreNotEqual(left, right);
        }

        [Test]
        public virtual void Empty_objects_are_equal()
        {
            //Arrange
            var left = new T();
            var right = new T();

            //Act / Assert
            AssertAreEqual(left, right);
        }

        public abstract void Non_empty_objects_are_equal();

        public abstract void Objects_are_not_equal();

        protected void AssertAreEqual(T left, T right)
        {
            //Act
            var areEqual = Equals(left, right); // left.Equals(right) and == won't work here

            //Assert
            Assert.IsTrue(areEqual);
        }

        protected void AssertAreNotEqual(T left, T right)
        {
            //Act
            var areEqual = Equals(left, right); // left.Equals(right) and == won't work here

            //Assert
            Assert.IsFalse(areEqual);
        }
    }
}
