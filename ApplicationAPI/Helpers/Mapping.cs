using ApplicationAPI.Models;
using ApplicationDB.Helper;
using ApplicationDB.Models;

namespace ApplicationAPI.Helpers
{
    public static class Mapping
    {
        public static Application ToApplicationDbModel(this ApplicationApiModel app) { 
        
            return new Application
            {
                Id = Guid.NewGuid(),
                UserId = app.UserId,
                Date = DateTime.Now.ToUniversalTime(),
                Activity = (TypeActivity)app.Activity,
                Name = app.Name,
                Description = app.Description,
                Outline = app.Outline,
                Status = (Status)1
            };
        }
    }
}
