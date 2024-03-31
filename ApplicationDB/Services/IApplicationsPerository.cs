using ApplicationDB.Helper;
using ApplicationDB.Models;

namespace ApplicationDB.Services
{
    public interface IApplicationsPerository
    {
        List<Application> GetAll();
        Application TryGetById(Guid id);
        Application TryGetByUserId(Guid userId);
        void Add(Application application);
        void Update(Application application);
        void Delete(Guid id);
        List<Application> GetAllAfterDate(DateTime date, Status status);
    }
}
