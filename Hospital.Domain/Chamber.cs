namespace Hospital.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Hospital.Staff.Extensions;

    /// <summary>
    /// Палата.
    /// </summary>
    public class Chamber
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Chamber"/>.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="title"> Название. </param>
        /// <param name="authors"> Пациенты. </param>
        public Chamber(int id, string title, params Patient[] authors)
            : this(id, title, new HashSet<Patient>(authors))
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Chamber"/>.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="title"> Название. </param>
        /// <param name="authors"> Множество пациентов. </param>
        public Chamber(int id, string title, ISet<Patient> authors = null)
        {
            this.Id = id;

            this.Title = title.TrimOrNull() ?? throw new ArgumentOutOfRangeException(nameof(title));

            // NOTE: Перебираем множество пациентов или пустое множество (если передан null)
            foreach (var author in authors ?? Enumerable.Empty<Patient>())
            {
                this.Patients.Add(author);
                author.AddChamber(this);
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Chamber"/>.
        /// </summary>
        [Obsolete("For ORM", true)]
        protected Chamber()
        {
        }

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public virtual int Id { get; protected set; }

        /// <summary>
        /// Заголовок.
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Пациенты.
        /// </summary>
        public virtual ISet<Patient> Patients { get; protected set; } = new HashSet<Patient>();

        /// <inheritdoc/>
        public override string ToString() => $"{this.Title} {this.Patients.Join()}".Trim();
    }
}
