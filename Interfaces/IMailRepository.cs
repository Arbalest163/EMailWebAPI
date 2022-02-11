public interface IMailRepository : IDisposable
{ 
    Task<List<MailVm>> GetEMailsAsync();
    Task AddEMailAsync(Mail eMail);
    Task SaveAsync();
}

