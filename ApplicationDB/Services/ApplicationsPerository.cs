using ApplicationDB.Helper;
using ApplicationDB.Models;

namespace ApplicationDB.Services
{
    public class ApplicationsPerository : IApplicationsPerository
    {
        private readonly DatabaseContext _databaseContext;

        public ApplicationsPerository(DatabaseContext _databaseContext) 
        { 
            this._databaseContext = _databaseContext;
        }

        public void Add(Application application)
        {
            _databaseContext.Applications.Add(application);
            _databaseContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var application = _databaseContext.Applications.FirstOrDefault(x => x.Id == id);

            if (application != null)
            {
                _databaseContext.Applications.Remove(application);
                _databaseContext.SaveChanges();
            }
        }

        public List<Application> GetAll() => _databaseContext.Applications.ToList();

        public List<Application> GetAllAfterDate(DateTime date, Status status) => _databaseContext.Applications.Where(x => x.Date > date && x.Status == status).ToList();

        public Application TryGetById(Guid id) => _databaseContext.Applications.FirstOrDefault(x => x.Id == id);

        public Application TryGetByUserId(Guid userId) => _databaseContext.Applications.FirstOrDefault(x => x.UserId == userId);

        public void Update(Application application)
        {
            var existingAnketa = _databaseContext.Applications.FirstOrDefault(x => x.Id == application.Id);
            if (existingAnketa != null)
            {
                existingAnketa.Plan = application.Plan;
                existingAnketa.Status = application.Status;
                existingAnketa.Name = application.Name;
                existingAnketa.Description = application.Description;
                existingAnketa.Activity = application.Activity;
            }
            _databaseContext.SaveChanges();
        }
    }
}
