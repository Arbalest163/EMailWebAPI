var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();

Configure(app);

var apis = app.Services.GetServices<IApi>();
foreach (var api in apis)
{
    if (api is null) throw new InvalidProgramException("Api not found");
    api.Register(app);
}

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddDbContext<MailDBContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
    });
    services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
    services.AddScoped<IMailRepository, MailRepository>();
    services.AddScoped<ISenderService, MailSenderService>();
    services.AddTransient<IApi, MailApi>();
}

void Configure(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty;
        });
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MailDBContext>();
        db.Database.Migrate();
    }

    app.UseHttpsRedirection();
}

