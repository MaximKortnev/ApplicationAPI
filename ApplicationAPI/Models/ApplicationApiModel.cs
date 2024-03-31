using ApplicationAPI.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ApplicationAPI.Models
{
    public class ApplicationApiModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Укажите id пользователя")]
        public Guid UserId { get; set; }
        public TypeActivityApiEnum? Activity { get; set; }

        [Required(ErrorMessage = "Укажите название")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Название должно быть не более 100 символов")]
        public string? Name { get; set; }

        [StringLength(300, MinimumLength = 1, ErrorMessage = "Описание должно быть не более 300 символов")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Укажите план")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Название должно быть не более 1000 символов")]
        public string? Outline { get; set; }
    }
}
