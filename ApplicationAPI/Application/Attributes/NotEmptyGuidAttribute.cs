using System.ComponentModel.DataAnnotations;

namespace Application.Application.Attributes
{
    /// <summary>
    /// Проверяет, что значение поля является действительным и не пустым идентификатором GUID.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class NotEmptyGuidAttribute : ValidationAttribute
    {
        /// <summary>
        /// Проверяет, что значение поля является действительным и не пустым идентификатором GUID.
        /// </summary>
        /// <param name="value">Значение, которое требуется проверить.</param>
        /// <param name="validationContext">Контекст валидации.</param>
        /// <returns>Результат валидации.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is Guid))
            {
                return new ValidationResult(ErrorMessage ?? "Поле должно содержать действительный идентификатор GUID.");
            }

            Guid guidValue = (Guid)value;
            if (guidValue == Guid.Empty)
            {
                return new ValidationResult(ErrorMessage ?? "Поле не может быть пустым.");
            }

            return ValidationResult.Success;
        }
    }
}