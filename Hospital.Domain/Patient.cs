namespace Hospital.Domain
{
    using System;
    using Hospital.Staff.Extensions;

    /// <summary>
    /// Пациент.
    /// </summary>
    public class Patient : IEquatable<Patient>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Patient"/>.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="chamber"> Палата. </param>
        /// <param name="fullName"> ФИО. </param>
        /// <param name="birthDate"> Дата рождения. </param>
        /// <param name="diagnosis"> Диагноз. </param>
        /// <param name="policy"> Номер полиса. </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В случае если <paramref name="fullName"/> <see langword="null"/>, пустая строка
        /// или строка, содержащая только пробельные символы.
        /// </exception>
        public Patient(int id, Chamber chamber, string fullName, DateTime birthDate, string diagnosis, uint policy)
        {
            if (id < 0)
            {
                throw new ArgumentException("ID cannot be negative.");
            }

            this.Id = id;
            this.Chamber = chamber;
            this.FullName = fullName.TrimOrNull() ?? throw new ArgumentOutOfRangeException(nameof(fullName));
            this.BirthDate = birthDate;
            this.Diagnosis = diagnosis;
            this.Policy = policy;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Patient"/>.
        /// </summary>
        [Obsolete("For ORM", true)]
        protected Patient()
        {
        }

        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public virtual int Id { get; protected set; }

        /// <summary>
        /// Палата.
        /// </summary>
        public virtual Chamber Chamber { get; protected set; }

        /// <summary>
        /// ФИО.
        /// </summary>
        public virtual string FullName { get; protected set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public virtual DateTime BirthDate { get; protected set; }

        /// <summary>
        /// Диагноз.
        /// </summary>
        public virtual string Diagnosis { get; protected set; }

        /// <summary>
        /// Номер полиса.
        /// </summary>
        public virtual uint Policy { get; protected set; }

        /// <inheritdoc/>
        public override string ToString() => this.FullName;

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && (ReferenceEquals(this, obj) || this.Equals(obj as Patient));
        }

        /// <inheritdoc cref="IEquatable{T}"/>
        public virtual bool Equals(Patient other)
        {
            return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || this.Id == other.Id);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => this.Id;
    }
}
