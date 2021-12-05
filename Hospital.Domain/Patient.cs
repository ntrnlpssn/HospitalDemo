namespace Hospital.Domain
{
    using System;
    using System.Collections.Generic;
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
        /// <param name="lastName"> Фамилия. </param>
        /// <param name="firstName"> Имя. </param>
        /// <param name="middleName"> Отчество. </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В случае если <paramref name="lastName"/> или <paramref name="firstName"/> <see langword="null"/>, пустая строка
        /// или строка, содержащая только пробельные символы.
        /// </exception>
        public Patient(int id, string lastName, string firstName, string middleName = null)
        {
            this.Id = id;
            this.FirstName = firstName.TrimOrNull() ?? throw new ArgumentOutOfRangeException(nameof(firstName));
            this.LastName = lastName.TrimOrNull() ?? throw new ArgumentOutOfRangeException(nameof(lastName));
            this.MiddleName = middleName.TrimOrNull();
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
        /// Имя.
        /// </summary>
        public virtual string FirstName { get; protected set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public virtual string LastName { get; protected set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public virtual string MiddleName { get; protected set; }

        /// <summary>
        /// Полное имя.
        /// </summary>
        public virtual string FullName => $"{this.LastName} {this.FirstName[0]}. {this.MiddleName?[0]}.".Trim();

        /// <summary>
        /// Множество палат.
        /// </summary>
        public virtual ISet<Chamber> Chambers { get; protected set; } = new HashSet<Chamber>();

        /// <summary>
        /// Метод, добавляющий палату пациенту.
        /// </summary>
        /// <param name="book"> Добавляемая палата. </param>
        /// <returns>
        /// Флаг успешности выполнения операции:
        /// <see langword="true"/> – палата была успешно добавлена,
        /// <see langword="false"/> в противном случае.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="book"/> – <see langword="null"/>.
        /// </exception>
        public virtual bool AddChamber(Chamber book)
        {
            return book == null
                ? throw new ArgumentNullException(nameof(book))
                : this.Chambers.Add(book);
        }

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
