public interface ISenderService
{
    Task<ISendResult> SendAsync(IRequest request);
}

