namespace Hospital.Demo
{
    using System;
    //// using System.Configuration;
    using System.Linq;
    using System.Collections.Generic;

    using Hospital.DataAccess;
    using Hospital.DataAccess.Repositories;
    using Hospital.Domain;

    /// <summary>
    /// Точка входа в программу.
    /// </summary>
    internal class Program
    {
        private static void Main()
        {
            int count = 400;
            var chambers = new List<Chamber>();
            for (int i = 0; i < count; i++)
            {
                uint capacity = 0;
                if (i % 100 == 0) { capacity += 2; }

                chambers.Add(new Chamber(i, (uint)i + 1, capacity));
            }

            DateTime dateOfBirth = new DateTime(1995, 5, 13);
            var patient = new Patient(1, chambers[201], "Иванов Иван Николаевич", dateOfBirth, "ангина", 5643);

            Console.WriteLine($"{chambers[201]} {patient}");

            //// var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            var settings = new Settings();

            settings.AddDatabaseServer(@"LAPTOP-2ALR8J1J\SQLEXPRESS");

            settings.AddDatabaseName("SecuredHospital");

            using var sessionFactory = Configurator.GetSessionFactory(settings, showSql: true);

            using (var session = sessionFactory.OpenSession())
            {
                chambers.ForEach(ch => session.Save(ch));
                session.Save(patient);
                session.Flush();
            }

            using (var session = sessionFactory.OpenSession())
            {
                var repo = new ChamberRepository();

                // TODO: Для наглядности нужно много палат с разным пациентским составом!
                Console.WriteLine("Results through repo:");
                repo.Filter(session, ch => ch.Capacity == 4)
                    .Distinct()
                    .ToList()
                    .ForEach(Console.WriteLine);
                Console.WriteLine(new string('-', 25));
            }

            using (var session = sessionFactory.OpenSession())
            {
                var tmpChamber = session.Query<Chamber>().First();

                Console.WriteLine(tmpChamber);
            }

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Clear();
                    var persistentPatient = session.Load<Patient>(1);
                    var newChamber = persistentPatient.Chamber;
                    if (newChamber is null)
                    {
                        throw new ArgumentNullException(nameof(newChamber));
                    }

                    var persistentChamber = session.Get<Chamber>(newChamber.Id);
                    session.Delete(persistentChamber);
                    transaction.Commit();
                }

                session.Flush();
            }
        }
    }
}
