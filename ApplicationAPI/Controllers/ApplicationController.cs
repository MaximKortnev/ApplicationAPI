using ApplicationAPI.Helpers;
using ApplicationAPI.Models;
using ApplicationDB.Helper;
using ApplicationDB.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApplicationAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationsRepository applicationRepositiry;

        public ApplicationController(IApplicationsRepository applicationRepositiry) 
        {
            this.applicationRepositiry = applicationRepositiry;
        }

        /// <summary>
        /// Создание новой заявки.
        /// </summary>
        /// <param name="request">Модель заявки</param>
        /// <returns>Созданна заявка или ошибка.</returns>
        [HttpPost]
        public ActionResult<ApplicationApiModel> CreateApplication(ApplicationApiModel request)
        {
            if (!ModelState.IsValid || request.UserId == Guid.Empty)
            {
                ModelState.AddModelError("UserId", "Не указан id пользователя");
                return BadRequest(ModelState);
            }
            if (request.Name == null && request.Description == null && request.Activity == null && request.Outline == null) {
                ModelState.AddModelError("UserId", "Заполните хотя бы одно поле");
                return BadRequest(ModelState);
            }
            var app = applicationRepositiry.TryGetByUserId(request.UserId);
            if (app == null)
            {
                var application = request.ToApplicationDbModel();

                applicationRepositiry.Add(application);
                return CreatedAtAction(nameof(GetApplicationById), new { id = application.Id }, application);
            }

            ModelState.AddModelError("UserId", "Заявка уже существует для указанного пользователя.");
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Редактироание.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        /// <returns>Отредактированная заявка или ошибка.</returns>
        [HttpPut("{id}")]
        public ActionResult<ApplicationApiModel> UpdateApplication(Guid id)
        {
            var application = applicationRepositiry.GetAll().FirstOrDefault(x => x.Id == id);

            if (application == null)
                return NotFound();
            if (application.Status == (Status)2) {
                ModelState.AddModelError("UserId", "Заявка уже отправлена, ее нельзя отредактировать.");
                return BadRequest(ModelState);
            }

            return Ok(application);
        }


        [HttpPost]
        public ActionResult<ApplicationApiModel> UpdateApplication(ApplicationApiModel request)
        {
            if (!ModelState.IsValid || request.UserId == Guid.Empty)
            {
                ModelState.AddModelError("UserId", "Не указан id пользователя");
                return BadRequest(ModelState);
            }
            if (request.Name == null && request.Description == null && request.Activity == null && request.Outline == null)
            {
                ModelState.AddModelError("UserId", "Заполните хотя бы одно поле");
                return BadRequest(ModelState);
            }
            var application = applicationRepositiry.GetAll().FirstOrDefault(x => x.Id == request.Id);

            if (application == null)
            {
                return NotFound();
            }

            applicationRepositiry.Update(request.ToApplicationDbModel());
            return Ok(application);
        }

        /// <summary>
        /// Удаление.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        /// <returns>Успешное удаление или ошибка.</returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteApplication(Guid id)
        {
            var application = applicationRepositiry.GetAll().FirstOrDefault(x => x.Id == id);
            if (application == null)
            {
                return NotFound();
            }
            if (application.Status == (Status)2)
            {
                ModelState.AddModelError("UserId", "Заявка уже отправлена, ее нельзя удалить.");
                return BadRequest(ModelState);
            }

            applicationRepositiry.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Отправка на рассмотрение.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        /// <returns>Успешная отправка или ошибка.</returns>
        [HttpPost("{id}/submit")]
        public ActionResult SubmitApplication(Guid id)
        {
            var application = applicationRepositiry.GetAll().FirstOrDefault(x => x.Id == id);
            if (application == null)
                return NotFound();
            if (application.Name.Length > 100)
            {
                ModelState.AddModelError("UserId", "Название должно быть не больше 100 символов.");
                return BadRequest(ModelState);
            }
            if (application.Description.Length > 300)
            {
                ModelState.AddModelError("UserId", "Описание должно быть не больше 300 символов.");
                return BadRequest(ModelState);
            }
            if (application.Outline.Length > 1000)
            {
                ModelState.AddModelError("UserId", "План должен быть не больше 1000 символов.");
                return BadRequest(ModelState);
            }
            if (application.Name == null && application.Activity == null && application.Outline == null)
            {
                ModelState.AddModelError("UserId", "Заполните все обязательные поля");
                return BadRequest(ModelState);
            }

            application.Status = (Status)2;
            applicationRepositiry.Update(application);
            return Ok();
        }

        /// <summary>
        /// Получение заявок поданных после указанной даты.
        /// </summary>
        /// <param name="submittedAfter">Дата для фильтрации.</param>
        /// <returns>Список заявок или ошибка.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ApplicationApiModel>> GetApplications([FromQuery] DateTime submittedAfter)
        {
            var application = applicationRepositiry.GetAllAfterDate(submittedAfter, (Status)2);
            return Ok(application);
        }

        /// <summary>
        /// Получение заявок не поданных и старше определенной даты.
        /// </summary>
        /// <param name="unsubmittedOlder">Дата для фильтрации.</param>
        /// <returns>Список заявок или ошибка.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ApplicationApiModel>> GetUnsubmittedApplications([FromQuery] DateTime unsubmittedOlder)
        {
            var application = applicationRepositiry.GetAllAfterDate(unsubmittedOlder, (Status)1);
            return Ok(application);
        }

        /// <summary>
        /// Получение текущей не поданной заявки для указанного пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Текущая заявка или ошибка.</returns>
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

        /// <summary>
        /// Получение заявки по id.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        /// <returns>Заявка или ошибка.</returns>
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

        /// <summary>
        /// Получение списка возможных типов активности.
        /// </summary>
        /// <returns>Список возможных типов активности.</returns>
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
