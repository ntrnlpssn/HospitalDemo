namespace Hospital.DataAccess
{
    using System.Reflection;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;

    /// <summary>
    /// Класс для настройки соединения для тестов.
    /// </summary>
    public class TestsConfigurator
    {
        private static Configuration configuration;

        /// <summary>
        /// Создание фабрики сессий.
        /// </summary>
        /// <param name="assembly"> Целевая сборка. </param>
        /// <param name="showSql"> Показывать генерируемый SQL-код. </param>
        /// <returns> Фабрику сессий. </returns>
        public static ISessionFactory GetSessionFactory(Assembly assembly = null, bool showSql = false)
        {
            var databaseConfiguration = SQLiteConfiguration.Standard.InMemory();

            if (showSql)
            {
                databaseConfiguration = databaseConfiguration.ShowSql().FormatSql();
            }

            return Fluently.Configure()
                .Database(databaseConfiguration)
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(assembly ?? Assembly.GetExecutingAssembly()))
                .ExposeConfiguration(c => configuration = c)
                .BuildSessionFactory();
        }

        /// <summary>
        /// Генерируется сессия для модульных тестов.
        /// </summary>
        /// <param name="showSql"> Показывать генерируемый SQL-код. </param>
        /// <returns> Сессия подключения к тестовой БД. </returns>
        public static ISession BuildSessionForTest(bool showSql = true)
        {
            var session = GetSessionFactory(showSql: showSql).OpenSession();
            new SchemaExport(configuration)
                .Execute(true, true, false, session.Connection, null);
            return session;
        }
    }
}