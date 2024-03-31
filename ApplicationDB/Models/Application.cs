using ApplicationDB.Helper;

namespace ApplicationDB.Models
{
    public class Application
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public Guid UserId { get; set; }
        public TypeActivity? Activity { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Plan { get; set; }
        public Status Status { get; set; } = (Status)1;
    }
}
