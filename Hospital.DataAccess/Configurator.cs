using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.DataAccess
{
    using System.Reflection;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate;
    using NHibernate.Tool.hbm2ddl;

    /// <summary>
    /// Настройщик подключения к БД и поставщик фабрики сессий.
    /// </summary>
    public static class Configurator
    {
        /// <summary>
        /// Конфигурация.
        /// </summary>
        private static FluentConfiguration fluentConfiguration;

        /// <summary>
        /// Генерирует фабрику сессий (<see cref="ISessionFactory"/>).
        /// </summary>
        /// <param name="settings"> Настройки. </param>
        /// <param name="assembly"> Целевая сборка. </param>
        /// <param name="showSql"> Показывать генерируемый SQL-код. </param>
        /// <returns> Фабрику сессий. </returns>
        public static ISessionFactory GetSessionFactory(
            Settings settings,
            Assembly assembly = null,
            bool showSql = false)
        {
            return GetConfiguration(settings, assembly ?? Assembly.GetExecutingAssembly(), showSql)
                .BuildSessionFactory();
        }

        /// <summary>
        /// Возвращаем конфигурацию по правилам.
        /// </summary>
        /// <param name="settings"> Установки названия сервера БД и имени БД. </param>
        /// <param name="assembly"> Целевая сборка. </param>
        /// <param name="showSql"> Показывать генерируемый SQL-код. </param>
        /// <returns> конфигурацию по правилам. </returns>
        private static FluentConfiguration GetConfiguration(
            Settings settings,
            Assembly assembly,
            bool showSql = false)
        {
            if (fluentConfiguration is null)
            {
                var databaseConfiguration = MsSqlConfiguration.MsSql2012.ConnectionString(
                    x => x
                        .Server(settings.GetDatabaseServer())
                        .Database(settings.GetDatabaseName())
                        .TrustedConnection());

                if (showSql)
                {
                    databaseConfiguration = databaseConfiguration.ShowSql().FormatSql();
                }

                fluentConfiguration = Fluently.Configure()
                    .Database(databaseConfiguration)
                    .Mappings(m => m.FluentMappings.AddFromAssembly(assembly))
                    .ExposeConfiguration(BuildSchema);
            }

            return fluentConfiguration;
        }

        /// <summary>
        /// Метод, порождающий таблицы (если их не было в БД) по конфигурации.
        /// </summary>
        /// <remarks> Необходимо только для создания схемы БД из ничего. </remarks>
        /// <param name="configuration"> Конфигурация ORM, содержащая правила отображения. </param>
        private static void BuildSchema(NHibernate.Cfg.Configuration configuration)
        {
            new SchemaExport(configuration).Execute(true, true, false);
        }
    }
}
