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
        /// <param name="number"> Номер палаты. </param>
        /// <param name="capacity"> Вместимость палаты. </param>
        public Chamber(int id, int number, int capacity)
        {
            this.Id = id;
            this.Number = number;
            this.Capacity = capacity;
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
        /// Номер палаты.
        /// </summary>
        public int Number { get; protected set; }

        /// <summary>
        /// Вместимость.
        /// </summary>
        /// 
        public int Capacity { get; protected set; }

        /// <inheritdoc/>
        public override string ToString() => $"{this.Number} {this.Capacity}".Trim();
    }
}
