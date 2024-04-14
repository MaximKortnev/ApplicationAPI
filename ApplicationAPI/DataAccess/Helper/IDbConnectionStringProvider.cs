namespace Application.DataAccess.Helper
{
    /// <summary>
    /// Интерфейс для предоставления строки подключения к базе данных.
    /// </summary>
    public interface IDbConnectionStringProvider
    {
        /// <summary>
        /// Получает строку подключения к базе данных.
        /// </summary>
        /// <returns>Строка подключения к базе данных.</returns>
        string GetConnectionString();
    }
}