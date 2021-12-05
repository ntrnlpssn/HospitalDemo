namespace Hospital.DataAccess.Tests
{
    using NHibernate;
    using NUnit.Framework;

    /// <summary>
    /// Базовый класс для тестирования маппингов.
    /// </summary>
    public class BaseMapTests
    {
        protected ISession Session { get; private set; }

        [SetUp]
        public void SetUp()
        {
            this.Session = TestsConfigurator.BuildSessionForTest();
        }

        [TearDown]
        public void TearDown()
        {
            this.Session?.Dispose();
        }
    }
}