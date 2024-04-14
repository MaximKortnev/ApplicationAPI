using Application.Application.Models;
using Application.DataAccess.Helper;
using Application.Domain.Models;

namespace Application.Application.Helpers
{
    /// <summary>
    /// Предоставляет методы для валидации полей моделей заявок.
    /// </summary>
    public class Validator
    {
        private readonly IConvertorEnum _convertorEnum;

        /// <summary>
        /// Инициализирует новый экземпляр класса Validator.
        /// </summary>
        /// <param name="convertorEnum">Экземпляр объекта для преобразования перечислений.</param>
        public Validator(IConvertorEnum convertorEnum)
        {
            _convertorEnum = convertorEnum;
        }

        /// <summary>
        /// Проверяет, являются ли поля модели заявки приложения заполненными с значениями по умолчанию.
        /// </summary>
        /// <param name="request">Модель заявки приложения.</param>
        /// <returns>True, если поля со значениями по умолчанию; в противном случае - false.</returns>
        public bool IsValidateField(ApplicationApiModel request)
        {
            if (request.Name == "Name" && _convertorEnum.ConvertToTypeActivityEnum(request.Activity) == TypeActivityApi.Default && request.Outline == "Outline")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверяет, являются ли поля модели заявки базы данных заполненными с значениями по умолчанию.
        /// </summary>
        /// <param name="application">Модель заявки базы данных.</param>
        /// <returns>True, если поля со значениями по умолчанию; в противном случае - false.</returns>
        public bool IsValidateFieldToSubmit(ApplicationDBModel application)
        {
            if (application.Name == "Name" && application.Activity == TypeActivity.Default && application.Outline == "Outline")
            {
                return true;
            }
            return false;
        }
    }
}