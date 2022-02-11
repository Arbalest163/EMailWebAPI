public class RequestDto
{
    /// <summary>
    /// Имя отправителя
    /// </summary>
    [Required]
    public string Subject { get; set; } = string.Empty;
    /// <summary>
    /// Тело письма
    /// </summary>
    [Required]
    public string Body { get; set; } = string.Empty;
    /// <summary>
    /// Адрес получателя
    /// </summary>
    [Required]
    public List<string> Recipients { get; set; } = new();
}

