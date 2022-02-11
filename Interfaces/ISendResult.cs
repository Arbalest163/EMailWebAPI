public enum ResultSender { Ok, Failed }

public interface ISendResult
{
    ResultSender Result { get; set; }
    string FailedMessage { get; set; }
}

