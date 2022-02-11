public class MailSenderService : ISenderService
{
    private readonly MailSettings _mailSettings;
    public MailSenderService(IOptions<MailSettings> options) => 
        _mailSettings = options.Value;

    /// <summary>
    /// Асинхроный метод отправки сообщения
    /// </summary>
    /// <param name="request">Отправляемое сообщение</param>
    /// <returns>Результат отправки сообщения</returns>
    public async Task<ISendResult> SendAsync(IRequest request)
    {
        var sendResult = new SendResult();
        try
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.Recipient));
            email.Subject = request.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = request.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);

            sendResult.Result = ResultSender.Ok;

            smtp.Disconnect(true);
        }
        catch (Exception ex)
        {
            sendResult.Result = ResultSender.Failed;
            sendResult.FailedMessage = ex.Message;
        }

        return sendResult;
    }
}

