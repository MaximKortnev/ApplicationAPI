using Application.DataAccess.Helper;

namespace Application.Domain.Models
{
    /// <summary>
    /// Модель заявки в базе данных.
    /// </summary>
    public class ApplicationDBModel
    {
        /// <summary>
        /// ID заявки.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания заявки.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Тип активности.
        /// </summary>
        public TypeActivity Activity { get; set; }

        /// <summary>
        /// Название заявки.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Описание заявки.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Описание планируемых действий.
        /// </summary>
        public required string Outline { get; set; }

        /// <summary>
        /// Статус заявки.
        /// </summary>
        public Status Status { get; set; } = (Status)1;
    }
}
