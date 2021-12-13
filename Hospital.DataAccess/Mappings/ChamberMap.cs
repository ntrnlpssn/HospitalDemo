namespace Hospital.DataAccess.Mappings
{
    using FluentNHibernate.Mapping;
    using Hospital.Domain;

    /// <summary>
    /// Класс, описывающий правила отображения <see cref="Chamber"/> на таблицу в БД и наоборот.
    /// </summary>
    internal class ChamberMap : ClassMap<Chamber>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChamberMap"/> class.
        /// </summary>
        public ChamberMap()
        {
            this.Table("Chambers");

            this.Id(x => x.Id);
            this.Map(x => x.Capacity);
            this.Map(x => x.Number);
            this.HasMany(x => x.Patients)
                .Cascade.Delete()
                .Not.Inverse();
        }
    }
}