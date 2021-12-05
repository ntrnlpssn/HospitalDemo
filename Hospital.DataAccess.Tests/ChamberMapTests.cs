namespace Hospital.DataAccess.Tests
{
    using FluentNHibernate.Testing;
    using Hospital.Domain;
    using NUnit.Framework;

    /// <summary>
    /// Тесты на правила отображения <see cref="Hospital.DataAccess.Mappings.ChamberMap"/>.
    /// </summary>
    [TestFixture]
    internal class ChamberMapTests : BaseMapTests
    {
        [Test]
        public void PersistenceSpecification_ValidData_Success()
        {
            // arrange
            var book = new Chamber(1, "Тестовая палата");

            // act & assert
            new PersistenceSpecification<Chamber>(this.Session)
                .VerifyTheMappings(book);
        }
    }
}