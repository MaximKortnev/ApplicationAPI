using System.ComponentModel.DataAnnotations;

namespace Application.Application.Attributes
{
    /// <summary>
    /// Проверяет, что значение поля не содержит только пробельные символы.
    /// </summary>
    public class NoWhitespaceAttribute : ValidationAttribute
    {
        /// <inheritdoc/>
        public override bool IsValid(object? value)
        {
            if (value is string str)
            {
                return !string.IsNullOrWhiteSpace(str);
            }

            return true;
        }
    }
}