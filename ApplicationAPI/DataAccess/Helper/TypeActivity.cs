using System.ComponentModel.DataAnnotations;

namespace Application.DataAccess.Helper
{
    /// <summary>
    /// Представляет типы активности.
    /// </summary>
    public enum TypeActivity
    {
        /// <summary>
        /// Значение по умолчанию.
        /// </summary>
        Default,

        /// <summary>
        /// Доклад.
        /// </summary>
        [Display(Name = "Report")]
        Report,

        /// <summary>
        /// Мастер-класс.
        /// </summary>
        [Display(Name = "MasterClass")]
        MasterClass,

        /// <summary>
        /// Дискуссия.
        /// </summary>
        [Display(Name = "Discussion")]
        Discussion
    }
}