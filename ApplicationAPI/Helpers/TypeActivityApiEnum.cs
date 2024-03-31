using System.ComponentModel.DataAnnotations;

namespace ApplicationAPI.Helpers
{
    public enum TypeActivityApiEnum
    {
        Default = 0,

        [Display(Name = "Доклад, 35-45 минут")]
        Report = 1,

        [Display(Name = "Мастеркласс, 1-2 часа")]
        MasterClass = 2,

        [Display(Name = "Дискуссия / круглый стол, 40-50 минут")]
        Discussion = 3
    }
}
