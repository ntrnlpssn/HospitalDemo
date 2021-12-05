using System;
using System.Collections.Generic;
using System.Text;

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
/*            this.Table("Patients");

            this.Id(x => x.Id);

            this.Map(x => x.Title);*/

            // see https://stackoverflow.com/a/713666/17310482
            this.HasManyToMany(x => x.Patients)
                .Cascade.Delete()
                .Inverse();
        }
    }
}
