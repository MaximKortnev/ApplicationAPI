using System.ComponentModel.DataAnnotations;

namespace Application.Application.Attributes
{
    /// <summary>
    /// Проверяет, что значение поля является действительным строковым представлением GUID.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValidGuidAttribute : ValidationAttribute
    {
        /// <summary>
        /// Проверяет, что значение поля является действительным строковым представлением GUID.
        /// </summary>
        /// <param name="value">Значение поля.</param>
        /// <param name="validationContext">Контекст валидации.</param>
        /// <returns>Результат валидации.</returns>
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || !(value is string))
            {
                return new ValidationResult(ErrorMessage ?? "Поле должно содержать строковое представление GUID.");
            }

            if (!Guid.TryParse((string)value, out _))
            {
                return new ValidationResult(ErrorMessage ?? "Значение не является действительным GUID.");
            }

            return ValidationResult.Success;
        }
    }
}
