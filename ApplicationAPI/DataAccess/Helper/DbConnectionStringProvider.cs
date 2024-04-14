namespace Application.DataAccess.Helper
{
    /// <summary>
    /// Предоставляет метод для получения строки подключения к базе данных.
    /// </summary>
    public class DbConnectionStringProvider : IDbConnectionStringProvider
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Инициализирует новый экземпляр класса DbConnectionStringProvider.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        public DbConnectionStringProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Получает строку подключения к базе данных.
        /// </summary>
        /// <returns>Строка подключения к базе данных.</returns>
        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("application_api");
        }
    }
}