public class MailApi : IApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("api/mails", Get)
            .Produces<List<MailVm>>(StatusCodes.Status200OK)
            .WithName("GetEmails")
            .WithTags("Getters");

        app.MapPost("api/mails", Post)
            .Accepts<RequestDto>("application/json")
            .Produces<List<MailVm>>(StatusCodes.Status201Created)
            .WithName("CreateEMail")
            .WithTags("Creators");
    }

    private async Task<IResult> Get(IMailRepository repository) =>
        Results.Ok(await repository.GetEMailsAsync());

    private async Task<IResult> Post([FromBody] RequestDto request, IMailRepository repository, ISenderService sender)
    {
        var emails = new List<MailVm>();

        foreach (var emailRecipient in request.Recipients)
        {
            var emailRequest = new MailRequest
            {
                Subject = request.Subject,
                Body = request.Body,
                Recipient = emailRecipient
            };
            var resultSender = await sender.SendAsync(emailRequest);

            var email = new Mail
            {
                Subject = emailRequest.Subject,
                Body = emailRequest.Body,
                Recipient = emailRequest.Recipient,
                Created = DateTimeOffset.Now,
                Result = resultSender.Result switch
                {
                    ResultSender.Ok => "Ok",
                    ResultSender.Failed => "Failed",
                    _ => ""
                },
                FailedMessage = resultSender.FailedMessage
            };
            emails.Add(Mapping(email));
            await repository.AddEMailAsync(email);
        }
        await repository.SaveAsync();

        return Results.Created($"/mails/", emails);
    }

    private MailVm Mapping(Mail mail)
    {
        return new MailVm
        {
            Subject = mail.Subject,
            Body = mail.Body,
            Recipient = mail.Recipient,
            Created = mail.Created,
            Result = mail.Result,
            FailedMessage = mail.FailedMessage
        };
    }

}

