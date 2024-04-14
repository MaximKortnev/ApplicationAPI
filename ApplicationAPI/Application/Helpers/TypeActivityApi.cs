using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Application.Helpers
{
    /// <summary>
    /// Представляет типы активности для API.
    /// </summary>
    public enum TypeActivityApi
    {
        /// <summary>
        /// Значение по умолчанию.
        /// </summary>
        Default,

        /// <summary>
        /// Доклад, 35-45 минут.
        /// </summary>
        [Display(Name = "Доклад, 35-45 минут")]
        [Description("Report")]
        Report,

        /// <summary>
        /// Мастеркласс, 1-2 часа.
        /// </summary>
        [Display(Name = "Мастеркласс, 1-2 часа")]
        [Description("MasterClass")]
        MasterClass,

        /// <summary>
        /// Дискуссия / круглый стол, 40-50 минут.
        /// </summary>
        [Display(Name = "Дискуссия / круглый стол, 40-50 минут")]
        [Description("Discussion")]
        Discussion
    }
}