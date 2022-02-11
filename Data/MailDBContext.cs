public class MailDBContext : DbContext
{
    public MailDBContext(DbContextOptions<MailDBContext> options) : base(options){}

    public DbSet<Mail> EMails => Set<Mail>();
}
