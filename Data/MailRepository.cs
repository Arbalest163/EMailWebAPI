public class MailRepository : IMailRepository
{
    private readonly MailDBContext _dBContext;

    public MailRepository(MailDBContext dBContext) => _dBContext = dBContext;

    /// <summary>
    /// Метод получения всех писем из базы данных
    /// </summary>
    /// <returns>Список писем</returns>
    public async Task<List<MailVm>> GetEMailsAsync() =>
        await _dBContext.EMails
            .Select(m => new MailVm
            {
                Subject = m.Subject,
                Body = m.Body,
                Recipient = m.Recipient,
                Created = m.Created,
                Result = m.Result,
                FailedMessage = m.FailedMessage
            })
            .ToListAsync();

    /// <summary>
    /// Метод добавления письма в базу данных
    /// </summary>
    /// <param name="eMail">Письмо</param>
    /// <returns></returns>
    public async Task AddEMailAsync(Mail eMail) =>
        await _dBContext.EMails.AddAsync(eMail);

    /// <summary>
    /// Метод сохранения данных в базу данных
    /// </summary>
    /// <returns></returns>
    public async Task SaveAsync() => await _dBContext.SaveChangesAsync();

    #region Очистка ресурсов Dispose

    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dBContext.Dispose();
            }
        }
        _disposed = true;
    }

    /// <summary>
    /// Метод очистки ресурсов
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}

