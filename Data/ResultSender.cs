public class SendResult : ISendResult
{
    /// <summary>
    /// Результат отправки сообщения
    /// </summary>
    public ResultSender Result { get; set; }
    /// <summary>
    /// Текст ошибки
    /// </summary>
    public string FailedMessage { get; set; } = string.Empty;
}

