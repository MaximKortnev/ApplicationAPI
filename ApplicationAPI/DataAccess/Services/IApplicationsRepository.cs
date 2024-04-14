using Application.Domain.Models;
using Application.DataAccess.Helper;

namespace Application.DataAccess.Services
{
    /// <summary>
    /// Интерфейс для работы с заявками.
    /// </summary>
    public interface IApplicationsRepository
    {
        /// <summary>
        /// Получение всех заявок.
        /// </summary>
        /// <returns>Список всех заявок.</returns>
        List<ApplicationDBModel> GetAll();

        /// <summary>
        /// Пытается получить заявку по её id.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        /// <returns>Заявка с указанным идентификатором или null, если заявка не найдена.</returns>
        ApplicationDBModel TryGetById(Guid id);

        /// <summary>
        /// Пытается получить заявку по id пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Заявка пользователя или null, если заявка не найдена.</returns>
        ApplicationDBModel TryGetByUserId(Guid userId);

        /// <summary>
        /// Добавляет новую заявку.
        /// </summary>
        /// <param name="application">Заявка для добавления.</param>
        void Add(ApplicationDBModel application);

        /// <summary>
        /// Обновляет информацию о заявке.
        /// </summary>
        /// <param name="application">Обновленная информация.</param>
        void Update(ApplicationDBModel application);

        /// <summary>
        /// Удаляет заявку по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заявки для удаления.</param>
        void Delete(Guid id);

        /// <summary>
        /// Получает все заявки, созданные после указанной даты.
        /// </summary>
        /// <param name="date">Дата, после которой созданы заявки.</param>
        /// <param name="status">Статус заявок для фильтрации.</param>
        /// <returns>Список заявок, соответствующих заданным критериям.</returns>
        List<ApplicationDBModel> GetAllAfterDate(DateTime date, Status status);
    }
}