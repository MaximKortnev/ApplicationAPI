using ApplicationAPI.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ApplicationAPI.Models
{
    public class ApplicationApiModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public TypeActivityApiEnum? Activity { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Outline { get; set; }
    }
}
