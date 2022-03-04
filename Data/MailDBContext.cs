public class MailDBContext : DbContext, IMailDBContext
{
    public MailDBContext(DbContextOptions<MailDBContext> options) : base(options) { }

    public DbSet<Mail> EMails => Set<Mail>();
}
