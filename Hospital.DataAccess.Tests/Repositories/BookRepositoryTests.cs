namespace Hospital.DataAccess.Tests.Repositories
{
    using Hospital.DataAccess.Repositories;
    using Hospital.Domain;
    using NHibernate;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для <see cref="ChamberRepository"/>.
    /// </summary>
    [TestFixture]
    public class ChamberRepositoryTests
    {
        [Test]
        public void Get_ValidId_Success()
        {
            // arrange
            var targetId = 1;

            using var session = GetSession();

            PrepareChamberInStorage(session, targetId);

            var repository = GetRepository();

            // act
            var result = repository.Get(session, targetId);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(targetId, result.Id);
        }

        private static void PrepareChamberInStorage(ISession session, int targetId)
        {
            var chamber = new Chamber(0, 1, 4);

            session.Save(chamber);
            session.Flush();
            session.Clear();
        }

        private static ChamberRepository GetRepository() => new ChamberRepository();

        private static ISession GetSession() => TestsConfigurator.BuildSessionForTest(showSql: true);
    }
}
