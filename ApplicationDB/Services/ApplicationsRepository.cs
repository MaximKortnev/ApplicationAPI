using ApplicationDB.Helper;
using ApplicationDB.Models;

namespace ApplicationDB.Services
{
    /// <summary>
    /// Реализация репозитория для работы с заявками.
    /// </summary>
    public class ApplicationsRepository : IApplicationsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ApplicationsRepository(DatabaseContext _databaseContext)
        {
            this._databaseContext = _databaseContext;
        }

        /// <summary>
        /// Добавляет новую заявку.
        /// </summary>
        /// <param name="application">Заявка для добавления.</param>
        public void Add(Application application)
        {
            _databaseContext.Applications.Add(application);
            _databaseContext.SaveChanges();
        }

        /// <summary>
        /// Удаляет заявку по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заявки для удаления.</param>
        public void Delete(Guid id)
        {
            var application = _databaseContext.Applications.FirstOrDefault(x => x.Id == id);

            if (application != null)
            {
                _databaseContext.Applications.Remove(application);
                _databaseContext.SaveChanges();
            }
        }

        /// <summary>
        /// Получение всех заявок.
        /// </summary>
        /// <returns>Список всех заявок.</returns>
        public List<Application> GetAll() => _databaseContext.Applications.ToList();

        /// <summary>
        /// Получает все заявки, созданные после указанной даты.
        /// </summary>
        /// <param name="date">Дата, после которой созданы заявки.</param>
        /// <param name="status">Статус заявок для фильтрации.</param>
        /// <returns>Список заявок, соответствующих заданным критериям.</returns>
        public List<Application> GetAllAfterDate(DateTime date, Status status) => _databaseContext.Applications.Where(x => x.Date > date && x.Status == status).ToList();

        /// <summary>
        /// Пытается получить заявку по её id.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        /// <returns>Заявка с указанным идентификатором или null, если заявка не найдена.</returns>
        public Application TryGetById(Guid id) => _databaseContext.Applications.FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Пытается получить заявку по id пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Заявка пользователя или null, если заявка не найдена.</returns>
        public Application TryGetByUserId(Guid userId) => _databaseContext.Applications.FirstOrDefault(x => x.UserId == userId);

        /// <summary>
        /// Обновляет информацию о заявке.
        /// </summary>
        /// <param name="application">Обновленная информация.</param>
        public void Update(Application application)
        {
            var existingAnketa = _databaseContext.Applications.FirstOrDefault(x => x.Id == application.Id);
            if (existingAnketa != null)
            {
                existingAnketa.Outline = application.Outline;
                existingAnketa.Status = application.Status;
                existingAnketa.Name = application.Name;
                existingAnketa.Description = application.Description;
                existingAnketa.Activity = application.Activity;
            }
            _databaseContext.SaveChanges();
        }
    }
}