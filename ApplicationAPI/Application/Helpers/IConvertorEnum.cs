using Application.DataAccess.Helper;

namespace Application.Application.Helpers
{
    /// <summary>
    /// Интерфейс для преобразования значений между строковыми представлениями и перечислениями типа активности.
    /// </summary>
    public interface IConvertorEnum
    {
        /// <summary>
        /// Преобразует строковое представление типа активности в перечисление TypeActivityApi.
        /// </summary>
        /// <param name="activity">Строковое представление типа активности.</param>
        /// <returns>Перечисление TypeActivityApi.</returns>
        TypeActivityApi ConvertToTypeActivityEnum(string activity);

        /// <summary>
        /// Преобразует перечисление типа активности в строковое представление.
        /// </summary>
        /// <param name="activity">Перечисление типа активности.</param>
        /// <returns>Строковое представление типа активности.</returns>
        string ConvertEnumToString(TypeActivity activity);
    }
}