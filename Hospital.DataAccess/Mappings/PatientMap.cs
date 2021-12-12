namespace Hospital.DataAccess.Mappings
{
    using FluentNHibernate.Mapping;
    using Hospital.Domain;

    /// <summary>
    /// Класс, описывающий правила отображения <see cref="Patient"/> на таблицу в БД и наоборот.
    /// </summary>
    internal class PatientMap : ClassMap<Patient>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PatientMap"/>.
        /// </summary>
        public PatientMap()
        {
            this.Table("Patients");

            this.Id(x => x.Id);
            this.Map(x => x.BirthDate);
            this.Map(x => x.Chamber);
            this.Map(x => x.Diagnosis);
            this.Map(x => x.FullName);
            this.Map(x => x.Policy);
        }
    }
}
