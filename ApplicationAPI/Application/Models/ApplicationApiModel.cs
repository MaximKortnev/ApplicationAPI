using Application.Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Application.Application.Models
{
    /// <summary>
    /// Модель для представления заявки через API.
    /// </summary>
    public class ApplicationApiModel
    {
        /// <summary>
        /// Уникальный id автора.
        /// </summary>
        [Required(ErrorMessage = "Не указан id пользователя")]
        [NoWhitespace(ErrorMessage = "Поле \"Автор\" не может состоять из пробелов")]
        [NotEmptyGuid(ErrorMessage = "Поле \"Автор\" не может быть пустым")]
        //[ValidGuidAttribute]
        public Guid Author { get; set; }

        /// <summary>
        /// Тип активности.
        /// </summary>
        [Required(ErrorMessage = "Не заполнено поле Активность")]
        [ValidActivity(ErrorMessage = "Неверно указано значение Активность")]
        public string Activity { get; set; } = "Default";

        /// <summary>
        /// Название.
        /// </summary>
        [Required(ErrorMessage = "Не указано название")]
        [NoWhitespace(ErrorMessage = "Название не может состоять из пробелов")]
        [StringLength(100, ErrorMessage = "Название не должно быть более 100 символов")]
        public string Name { get; set; } = "Name";

        /// <summary>
        /// Описание активности.
        /// </summary>
        [StringLength(300, ErrorMessage = "Описание не должно быть более 300 символов")]
        public string Description { get; set; } = String.Empty;

        /// <summary>
        /// План выступления.
        /// </summary>
        [Required(ErrorMessage = "Не указан id пользователя")]
        [NoWhitespace(ErrorMessage = "План не может состоять из пробелов")]
        [StringLength(1000, ErrorMessage = "План не должен быть более 1000 символов")]
        public string Outline { get; set; } = "Outline";
    }
}
