using Application.Application.Models;
using Application.Domain.Models;

namespace Application.Application.Helpers
{
    /// <summary>
    /// Интерфейс для преобразования моделей между уровнями приложения.
    /// </summary>
    public interface IMappings
    {
        /// <summary>
        /// Преобразует модель заявки приложения в модель заявки базы данных.
        /// </summary>
        /// <param name="app">Модель заявки приложения.</param>
        /// <returns>Модель заявки базы данных.</returns>
        ApplicationDBModel ToApplicationDbModel(ApplicationApiModel app);

        /// <summary>
        /// Преобразует модель заявки базы данных в модель заявки приложения.
        /// </summary>
        /// <param name="app">Модель заявки базы данных.</param>
        /// <returns>Модель заявки приложения.</returns>
        ApplicationRequestModel ToApplicationRequestModel(ApplicationDBModel app);

        /// <summary>
        /// Преобразует список моделей заявок базы данных в список моделей заявок приложения.
        /// </summary>
        /// <param name="apps">Список моделей заявок базы данных.</param>
        /// <returns>Список моделей заявок приложения.</returns>
        List<ApplicationRequestModel> ToApplicationsRequestModel(List<ApplicationDBModel> apps);
    }
}