using ApplicationDB.Helper;

namespace ApplicationDB.Models
{
    public class Application
    {
        /// <summary>
        /// ID заявки.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания заявки.
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Тип активности.
        /// </summary>
        public TypeActivity? Activity { get; set; }

        /// <summary>
        /// Название заявки.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Описание заявки.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Описание планируемых действий.
        /// </summary>
        public string? Outline { get; set; }

        /// <summary>
        /// Статус заявки.
        /// </summary>
        public Status Status { get; set; } = (Status)1;
    }
}
