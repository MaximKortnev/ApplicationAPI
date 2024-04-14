using System.ComponentModel.DataAnnotations;

namespace Application.Application.Attributes
{
    /// <summary>
    /// Проверяет, что значение поля "Активность" соответствует одному из заданных значений: "Report", "MasterClass" или "Discussion".
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValidActivityAttribute : ValidationAttribute
    {
        /// <summary>
        /// Проверяет, что значение поля "Активность" соответствует одному из заданных значений: "Report", "MasterClass" или "Discussion".
        /// </summary>
        /// <param name="value">Значение поля "Активность".</param>
        /// <param name="validationContext">Контекст валидации.</param>
        /// <returns>Результат валидации.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string activity = value.ToString();

            if (!string.Equals(activity, "Report", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(activity, "MasterClass", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(activity, "Discussion", StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult(ErrorMessage ?? "Неверно указано значение Активность.");
            }

            return ValidationResult.Success;
        }
    }
}
