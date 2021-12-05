using System;

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
        /// Инициализирует новый экземпляр класса <see cref="ChamberMap"/>.
        /// </summary>
        public ChamberMap()
        {
/*            this.Table("Chambers");

            this.Id(x => x.Id);

            this.Map(x => x.Title);*/

            // see https://stackoverflow.com/a/713666/17310482
            this.HasManyToMany(x => x.Patients)
                .Cascade.Delete()
                .Inverse();
        }
    }
}