public class MailRequest : IRequest
{
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
}
