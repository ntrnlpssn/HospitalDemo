using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Demo.Tests
{
    using System;
    using NUnit.Framework;
    using Hospital.Domain;

    [TestFixture]
    public class PatientTests
    {
        [Test]
        public void Constructor_ValidData_Success()
        {
            // arrange
            var chamber = new Chamber(1, 2, 3);

            // act & assert
            Assert.DoesNotThrow(() => _ = new Patient(1, chamber, "Аркадий Игоревич Носов", DateTime.Now, "бронхит", 4));
        }
        
        [Test]
        public void Constructor_NegativeID_Fail()
        {
            // arrange
            var chamber = new Chamber(1, 2, 3);

            // act & assert
            Assert.Throws<ArgumentException>(() => _ = new Patient(-1, chamber, "Аркадий Игоревич Носов", DateTime.Now, "бронхит", 4));
        }

        [Test]
        public void ToString_ValidData()
        {
            // arrange
            var chamber = new Chamber(1, 2, 3);
            var patient = new Patient(1, chamber, "Аркадий Игоревич Носов", DateTime.Now, "бронхит", 4);
            var expected = "Аркадий Игоревич Носов";

            // act
            var result = patient.ToString();

            // assert
            Assert.AreEqual(expected, result);
        }

    }
}
