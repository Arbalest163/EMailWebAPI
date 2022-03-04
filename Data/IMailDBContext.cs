
public interface IMailDBContext
{
    DbSet<Mail> EMails { get; }
}