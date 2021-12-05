namespace Hospital.Demo
{
    using System;
    //// using System.Configuration;
    using System.Linq;

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
            var author = new Patient(1, "Толстой", "Лев", "Николаевич");

            var book = new Chamber(1, "Война и мир", author);

            Console.WriteLine($"{book} {author}");

            //// var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            var settings = new Settings();

            settings.AddDatabaseServer(@"LAPTOP-2ALR8J1J\SQLEXPRESS");

            settings.AddDatabaseName("SecuredHospital");

            using var sessionFactory = Configurator.GetSessionFactory(settings, showSql: true);

            using (var session = sessionFactory.OpenSession())
            {
                session.Save(book);
                session.Save(author);
                session.Flush();
            }

            using (var session = sessionFactory.OpenSession())
            {
                var repo = new ChamberRepository();

                // TODO: Для наглядности нужно много палат с разным пациентским составом!
                Console.WriteLine("Results through repo:");
                repo.Filter(session, b => b.Patients.Count < 2)
                    .SelectMany(b => b.Patients)
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
                    var newChamber = persistentPatient.Chambers.FirstOrDefault();
                    if (newChamber is null)
                    {
                        throw new ArgumentNullException(nameof(newChamber));
                    }

                    var persistentChamber = session.Get<Chamber>(newChamber.Id);
                    //// persistentChamber.Title = "Война и миръ";
                    //// session.SaveOrUpdate(persistentChamber);
                    session.Delete(persistentChamber);
                    transaction.Commit();
                }

                session.Flush();
            }
            
        }
    }
}
