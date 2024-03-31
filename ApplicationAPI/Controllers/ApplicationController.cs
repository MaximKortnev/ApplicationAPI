using ApplicationAPI.Helpers;
using ApplicationAPI.Models;
using ApplicationDB.Helper;
using ApplicationDB.Models;
using ApplicationDB.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApplicationAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationsPerository applicationRepositiry;

        public ApplicationController(IApplicationsPerository applicationRepositiry) 
        {
            this.applicationRepositiry = applicationRepositiry;
        }
        // Создание заявки
        [HttpPost]
        public ActionResult<ApplicationApiModel> CreateApplication(ApplicationApiModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = new Application
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Activity = (TypeActivity)request.Activity,
                Name = request.Name,
                Description = request.Description,
                Plan = request.Plan,
                Status = (Status)1
            };

            applicationRepositiry.Add(application);

            return CreatedAtAction(nameof(GetApplicationById), new { id = application.Id }, application);
        }

        // Редактирование заявки
        [HttpPut("{id}")]
        public ActionResult<ApplicationApiModel> UpdateApplication(Guid id)
        {
            var application = applicationRepositiry.GetAll().FirstOrDefault(x => x.Id == id);

            if (application == null)
            {
                return NotFound();
            }

            applicationRepositiry.Update(application);
            return Ok(application);
        }

        // Удаление заявки
        [HttpDelete("{id}")]
        public ActionResult DeleteApplication(Guid id)
        {
            var application = applicationRepositiry.GetAll().FirstOrDefault(x => x.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            applicationRepositiry.Delete(id);
            return Ok();
        }

        // Отправка заявки на рассмотрение
        [HttpPost("{id}/submit")]
        public ActionResult SubmitApplication(Guid id)
        {
            var application = applicationRepositiry.GetAll().FirstOrDefault(x => x.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            application.Status = (Status)2;
            return Ok();
        }

        // Получение заявок поданных после указанной даты
        [HttpGet]
        public ActionResult<IEnumerable<ApplicationApiModel>> GetApplications([FromQuery] DateTime submittedAfter)
        {
            var application = applicationRepositiry.GetAllAfterDate(submittedAfter, (Status)2);
            return Ok(application);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ApplicationApiModel>> GetAll()
        {
            var application = applicationRepositiry.GetAll();
            return Ok(application);
        }

        // Получение заявок не поданных и старше определенной даты
        [HttpGet]
        public ActionResult<IEnumerable<ApplicationApiModel>> GetUnsubmittedApplications([FromQuery] DateTime unsubmittedOlder)
        {
            var application = applicationRepositiry.GetAllAfterDate(unsubmittedOlder, (Status)1);
            return Ok(application);
        }

        // Получение текущей не поданной заявки для указанного пользователя
        [HttpGet("users/{userId}/currentapplication")]
        public ActionResult<ApplicationApiModel> GetCurrentApplication(Guid userId)
        {
            var application = applicationRepositiry.TryGetByUserId(userId);
            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        // Получение заявки по идентификатору
        [HttpGet("{id}")]
        public ActionResult<ApplicationApiModel> GetApplicationById(Guid id)
        {
            var application = applicationRepositiry.TryGetById(id);
            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        // Получение списка возможных типов активности
        [HttpGet("activities")]
        public ActionResult<IEnumerable<string>> GetActivityTypes()
        {
            var activityTypes = Enum.GetValues(typeof(TypeActivityApiEnum))
                               .Cast<TypeActivityApiEnum>()
                               .Where(e => e != TypeActivityApiEnum.Default)
                               .Select(e => new ActivityDto
                               {
                                   Activity = e.ToString(),
                                   Description = GetDisplayName(e)
                               })
                               .ToList();

            return Ok(activityTypes);
        }
        private string GetDisplayName(TypeActivityApiEnum activity)
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
