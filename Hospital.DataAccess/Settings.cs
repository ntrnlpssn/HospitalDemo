namespace Hospital.DataAccess
{
    using System;
    using System.Configuration;

    public sealed class Settings
    {
        //private const string ServerNameKey = "databaseServer";

        //private readonly Configuration configuration;

        //public Settings(Configuration configuration)
        //{
        //    this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        //}

        private string databaseServerName;

        private string databaseName;

        public void AddDatabaseServer(string serverName)
        {
            this.databaseServerName = serverName;
            //this.configuration.AppSettings.Settings.Add(ServerNameKey, serverName);
        }

        public string GetDatabaseServer()
        {
            return this.databaseServerName;
            //return this.configuration.AppSettings.Settings[ServerNameKey].Value;
        }

        public void AddDatabaseName(string databaseName)
        {
            this.databaseName = databaseName;
        }

        public string GetDatabaseName()
        {
            return this.databaseName;
        }
    }
}
