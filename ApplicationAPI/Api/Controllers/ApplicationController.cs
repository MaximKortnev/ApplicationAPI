using Application.Application.Helpers;
using Application.DataAccess.Helper;
using Application.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;
using Application.Application.Models;
using Application.Contracts;

namespace Application.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с заявками.
    /// </summary>
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationsRepository applicationRepository;
        private readonly IMappings _mapper;
        private readonly ActivityDto _activityDto;
        private readonly Validator _validator;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера ApplicationController.
        /// </summary>
        /// <param name="applicationRepository">Репозиторий заявок.</param>
        /// <param name="mapper">Маппер для преобразования моделей.</param>
        /// <param name="activityDto">DTO для типов активности.</param>
        /// <param name="validator">Валидатор заявок.</param>
        public ApplicationController(IApplicationsRepository applicationRepository, IMappings mapper, ActivityDto activityDto, Validator validator)
        {
            this.applicationRepository = applicationRepository;
            _mapper = mapper;
            _activityDto = activityDto;
            _validator = validator;
        }

        /// <summary>
        /// Создание новой заявки.
        /// </summary>
        /// <param name="request">Модель заявки</param>
        /// <returns>Созданна заявка или ошибка.</returns>
        [HttpPost("/applications")]
        public ActionResult<ApplicationApiModel> Create(ApplicationApiModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_validator.IsValidateField(request))
            {
                return BadRequest("Заполните хотя бы одно поле");
            }
            var app = applicationRepository.TryGetByUserId(request.Author);
            if (app == null)
            {
                var application = _mapper.ToApplicationDbModel(request);

                applicationRepository.Add(application);
                return Ok(_mapper.ToApplicationRequestModel(application));
            }

            ModelState.AddModelError("UserId", "Заявка уже существует для указанного пользователя.");
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Редактироание.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        /// <returns>Отредактированная заявка или ошибка.</returns>
        [HttpPut("/applications/{id}")]
        public ActionResult<ApplicationApiModel> Update(Guid id, ApplicationApiModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = applicationRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (application == null)
            {
                return BadRequest("Заявка не найдена, возможно неверный id");
            }

            if (application.Status == Status.Sent)
            {
                ModelState.AddModelError("UserId", "Заявка уже отправлена, ее нельзя отредактировать.");
                return BadRequest(ModelState);
            }
           
            if (_validator.IsValidateField(request))
            {
                return BadRequest("Заполните хотя бы одно поле");
            }

            applicationRepository.Update(_mapper.ToApplicationDbModel(request));

            return Ok(_mapper.ToApplicationRequestModel(application));
        }

        /// <summary>
        /// Удаление.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        /// <returns>Успешное удаление или ошибка.</returns>
        [HttpDelete("/applications/{id}")]
        public ActionResult Delete(Guid id)
        {
            var application = applicationRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (application == null)
            {
                return BadRequest("Заявка не найдена, возможно неверный id");
            }
            if (application.Status == Status.Sent)
            {
                ModelState.AddModelError("UserId", "Заявка уже отправлена, ее нельзя удалить.");
                return BadRequest(ModelState);
            }

            applicationRepository.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Отправка на рассмотрение.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        /// <returns>Успешная отправка или ошибка.</returns>
        [HttpPost("/applications/{id}/submit")]
        public ActionResult Submit(Guid id)
        {
            if (!ModelState.IsValid || id == Guid.Empty)
            {
                return BadRequest(ModelState);
            }
            var application = applicationRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (application == null)
            {
                return BadRequest("Заявка не найдена, возможно ошибка в id");
            }

            if (_validator.IsValidateFieldToSubmit(application))
            {
                return BadRequest("Заполните все обязательные поля");
            }

            application.Status = Status.Sent;
            applicationRepository.Update(application);
            return Ok();
        }

        /// <summary>
        /// Получает список заявок, отправленных после указанной даты или неотправленных и созданных ранее указанной даты.
        /// </summary>
        /// <param name="submittedAfter">Дата, после которой были отправлены заявки.</param>
        /// <param name="unsubmittedOlder">Не поданные заявки старше даты. </param>
        /// <returns>Список заявок, удовлетворяющих заданным критериям.</returns>
        [HttpGet("/applications")]
        public ActionResult<IEnumerable<ApplicationApiModel>> GetAfterDate([FromQuery] DateTime? submittedAfter, [FromQuery] DateTime? unsubmittedOlder)
        {
            if (submittedAfter.HasValue)
            {
                var application = applicationRepository.GetAllAfterDate(submittedAfter.Value, Status.Sent);
                return Ok(_mapper.ToApplicationsRequestModel(application));
            }
            else if (unsubmittedOlder.HasValue)
            {
                var application = applicationRepository.GetAllAfterDate(unsubmittedOlder.Value, Status.Draft);
                return Ok(_mapper.ToApplicationsRequestModel(application));
            }
            else
            {
                return BadRequest("Необходимо передать хотя бы один из параметров: submittedAfter или unsubmittedOlder.");
            }
        }

        /// <summary>
        /// Получение текущей не поданной заявки для указанного пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Текущая заявка или ошибка.</returns>
        [HttpGet("users/{userId}/currentapplication")]
        public ActionResult<ApplicationApiModel> GetCurrentApplication(Guid userId)
        {
            var application = applicationRepository.TryGetByUserId(userId);
            if (application == null)
            {
                return BadRequest("Заявка не найдена, возможно ошибка в id");
            }

            return Ok(_mapper.ToApplicationRequestModel(application));
        }

        /// <summary>
        /// Получение заявки по id.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        /// <returns>Заявка или ошибка.</returns>
        [HttpGet("/applications/{id}")]
        public ActionResult<ApplicationApiModel> GetApplicationById(Guid id)
        {
            var application = applicationRepository.TryGetById(id);
            if (application == null)
            {
                return BadRequest("Заявка не найдена, возможно ошибка в id");
            }

            return Ok(_mapper.ToApplicationRequestModel(application));
        }

        /// <summary>
        /// Получение списка возможных типов активности.
        /// </summary>
        /// <returns>Список возможных типов активности.</returns>
        [HttpGet("/activities")]
        public ActionResult<IEnumerable<string>> GetActivityTypes()
        {
            var activityTypes = Enum.GetValues(typeof(TypeActivityApi))
                               .Cast<TypeActivityApi>()
                               .Where(e => e != TypeActivityApi.Default)
                               .Select(e => new ActivityDto
                               {
                                   Activity = e.ToString(),
                                   Description = _activityDto.GetDisplayName(e)
                               })
                               .ToList();

            return Ok(activityTypes);
        }
    }
}
