/// <summary>
/// Сущность модели базы данных
/// </summary>
public class Mail : Entity
{
    /// <summary>
    /// Имя отправителя
    /// </summary>
    public string Subject { get; set; } = string.Empty;
    /// <summary>
    /// Тело ообщения
    /// </summary>
    public string Body { get; set; } = string.Empty;
    /// <summary>
    /// Адрес получателя
    /// </summary>
    public string Recipient { get; set; } = string.Empty;
    /// <summary>
    /// Дата создания письма в UTC
    /// </summary>
    public DateTimeOffset Created { get; set; }
    /// <summary>
    /// Результат отправки сообщения
    /// </summary>
    public string Result { get; set; } = string.Empty;
    /// <summary>
    /// Текст ошибки
    /// </summary>
    public string FailedMessage { get; set; } = string.Empty;
}
