using Application.DataAccess.Helper;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Application.Application.Helpers
{
    /// <summary>
    /// Предоставляет методы для преобразования значений между строковыми представлениями и перечислениями типа активности.
    /// </summary>
    public class ConvertorEnum : IConvertorEnum
    {
        /// <summary>
        /// Преобразует строковое представление типа активности в перечисление TypeActivityApi.
        /// </summary>
        /// <param name="activity">Строковое представление типа активности.</param>
        /// <returns>Перечисление TypeActivityApi.</returns>
        public TypeActivityApi ConvertToTypeActivityEnum(string activity)
        {
            switch (activity)
            {
                case "Report":
                    return TypeActivityApi.Report;
                case "MasterClass":
                    return TypeActivityApi.MasterClass;
                case "Discussion":
                    return TypeActivityApi.Discussion;
                default:
                    return TypeActivityApi.Default;
            }
        }

        /// <summary>
        /// Преобразует перечисление типа активности в строковое представление.
        /// </summary>
        /// <param name="activity">Перечисление типа активности.</param>
        /// <returns>Строковое представление типа активности.</returns>
        public string ConvertEnumToString(TypeActivity activity)
        {
            FieldInfo field = activity.GetType().GetField(activity.ToString());
            DisplayAttribute displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name ?? activity.ToString();
        }
    }
}
