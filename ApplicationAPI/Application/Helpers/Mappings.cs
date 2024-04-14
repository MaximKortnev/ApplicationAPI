using Application.DataAccess.Helper;
using Application.Domain.Models;
using Application.Application.Models;

namespace Application.Application.Helpers
{
    /// <summary>
    /// Предоставляет методы для преобразования моделей между уровнями приложения.
    /// </summary>
    public class Mappings : IMappings
    {
        private readonly IConvertorEnum _convertorEnum;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Mappings"/>.
        /// </summary>
        /// <param name="convertorEnum">Экземпляр объекта для преобразования перечислений.</param>
        public Mappings(IConvertorEnum convertorEnum)
        {
            _convertorEnum = convertorEnum;
        }

        /// <summary>
        /// Преобразует модель заявки из API в модель заявки для базы данных.
        /// </summary>
        /// <param name="app">Модель заявки из API.</param>
        /// <returns>Модель заявки для базы данных.</returns>
        public ApplicationDBModel ToApplicationDbModel(ApplicationApiModel app)
        {
            return new ApplicationDBModel
            {
                Id = Guid.NewGuid(),
                UserId = app.Author,
                Date = DateTime.Now.ToUniversalTime(),
                Activity = (TypeActivity)_convertorEnum.ConvertToTypeActivityEnum(app.Activity),
                Name = app.Name,
                Description = app.Description,
                Outline = app.Outline,
                Status = Status.Draft
            };
        }

        /// <summary>
        /// Преобразует модель заявки из базы данных в модель заявки для запросов.
        /// </summary>
        /// <param name="app">Модель заявки из базы данных.</param>
        /// <returns>Модель заявки для запросов.</returns>
        public ApplicationRequestModel ToApplicationRequestModel(ApplicationDBModel app)
        {
            return new ApplicationRequestModel
            {
                Id = app.Id,
                Author = app.UserId,
                Activity = _convertorEnum.ConvertEnumToString(app.Activity),
                Name = app.Name,
                Description = app.Description,
                Outline = app.Outline,
            };
        }

        /// <summary>
        /// Преобразует список моделей заявок из базы данных в список моделей заявок для запросов.
        /// </summary>
        /// <param name="apps">Список моделей заявок из базы данных.</param>
        /// <returns>Список моделей заявок для запросов.</returns>
        public List<ApplicationRequestModel> ToApplicationsRequestModel(List<ApplicationDBModel> apps)
        {
            return apps.Select(x => ToApplicationRequestModel(x)).ToList();
        }
    }
}