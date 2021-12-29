namespace Hospital.Demo.Tests
{
    using System;
    using NUnit.Framework;
    using Hospital.Domain;

    [TestFixture]
    public class ChamberTests
    {
        [Test]
        public void ToString_ValidData_Success()
        {
            // arrange
            var chamber = new Chamber(0, 1, 2);
            var expected = "1 2";

            // act
            var actual = chamber.ToString();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Constructor_NegativeID_Fail()
        {
            // act & assert
            Assert.Throws<ArgumentException>(() => _ = new Chamber(-4, 1, 3));
        }

        [Test]
        public void Constructor_ValidData_Success()
        {
            // act & assert
            Assert.DoesNotThrow(() => _ = new Chamber(1, 5, 6));
        }
    }
}
