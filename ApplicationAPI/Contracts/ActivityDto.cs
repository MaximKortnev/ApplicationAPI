using Application.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Application.Contracts
{
    /// <summary>
    /// Представляет модель для активности.
    /// </summary>
    public class ActivityDto
    {
        /// <summary>
        /// Получает или задает строковое представление активности.
        /// </summary>
        public string Activity { get; set; }

        /// <summary>
        /// Получает или задает описание активности.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Получает отображаемое имя для указанной активности.
        /// </summary>
        /// <param name="activity">Тип активности.</param>
        /// <returns>Отображаемое имя активности.</returns>
        public string GetDisplayName(TypeActivityApi activity)
        {
            return activity.GetType()
                           .GetMember(activity.ToString())[0]
                           .GetCustomAttributes(typeof(DisplayAttribute), false)
                           .OfType<DisplayAttribute>()
                           .FirstOrDefault()?
                           .Name ?? activity.ToString();
        }
    }
}