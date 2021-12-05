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
            var author = new Patient(1, "Толстой", "Лев", "Николаевич");
            var book = new Chamber(1, "Война и мир", author);
            var expected = "Война и мир Толстой Л. Н.";

            //act
            var actual = book.ToString();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToString_EmptyPatient_Success()
        {
            // arrange
            var book = new Chamber(1, "Библия");
            var expected = "Библия";

            //act
            var actual = book.ToString();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Ctor_ValidDataEmptyPatients_Success()
        {
            // arrange & act & assert
            Assert.DoesNotThrow(() => _ = new Chamber(1, "Библия"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("\0")]
        [TestCase("\n")]
        [TestCase("\r")]
        [TestCase("\t")]
        public void Ctor_WrongDataNullTitleEmptyPatients_Fail(string wrongTitle)
        {
            // act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Chamber(1, wrongTitle));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("\0")]
        [TestCase("\n")]
        [TestCase("\r")]
        [TestCase("\t")]
        public void Ctor_WrongDataNullTitleEmptyPatient_Fail(string wrongTitle)
        {
            // arrange
            var author = new Patient(1, "Толстой", "Лев", "Николаевич");

            // act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Chamber(1, wrongTitle));
        }

        [Test]
        public void Ctor_ValidData_Success()
        {
            // arrange
            var author = new Patient(1, "Толстой", "Лев", "Николаевич");

            // act & assert
            Assert.DoesNotThrow(() => _ = new Chamber(1, "Война и мир", author));
        }
    }
}
