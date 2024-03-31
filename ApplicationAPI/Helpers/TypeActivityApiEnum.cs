using System.ComponentModel.DataAnnotations;

namespace ApplicationAPI.Helpers
{
    public enum TypeActivityApiEnum
    {
        Default,

        [Display(Name = "Доклад, 35-45 минут")]
        Report,

        [Display(Name = "Мастеркласс, 1-2 часа")]
        MasterClass,

        [Display(Name = "Дискуссия / круглый стол, 40-50 минут")]
        Discussion
    }
}
