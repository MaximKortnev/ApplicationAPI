namespace Application.DataAccess.Helper
{
    /// <summary>
    /// Представляет статус заявки.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Значение по умолчанию.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Черновик заявки.
        /// </summary>
        Draft = 1,

        /// <summary>
        /// Заявка отправлена.
        /// </summary>
        Sent = 2,
    }
}