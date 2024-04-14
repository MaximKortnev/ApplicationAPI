namespace Application.Application.Models
{
    /// <summary>
    /// Модель запроса заявки.
    /// </summary>
    public class ApplicationRequestModel
    {
        /// <summary>
        /// ID заявки.
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Author { get; set; }

        /// <summary>
        /// Тип активности.
        /// </summary>
        public required string Activity { get; set; }

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
    }
}
