using System.Reflection;
using AuthenticationService.Consumers;
using AuthenticationService.Data;
using AuthenticationService.Security;
using AuthenticationService.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IQueueService, QueueService>();
string connectionString = builder.Configuration.GetConnectionString("postgres");

builder.Services.AddDbContext<AuthenticationContext>(options =>
{
    //options.UseInMemoryDatabase("DefaultConnection");
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseNpgsql(connectionString);
    //options.UseSqlServer(connectionString);
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddMassTransit(x =>
{
    // x.SetKebabCaseEndpointNameFormatter();
    // x.SetInMemorySagaRepositoryProvider();
    //
    // var entryAssembly = Assembly.GetEntryAssembly();
    //
    // x.AddConsumers(entryAssembly);
    // x.AddSagaStateMachines(entryAssembly);
    // x.AddSagas(entryAssembly);
    // x.AddActivities(entryAssembly);
    
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host("Endpoint=sb://kwetter.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=z5cde4ad4vbhF2PmGK0pFTIinKs1rUInm13ogiJI6nU=");
        cfg.ConfigureEndpoints(context);
    });
    
    builder.Services.AddHostedService<Worker>();
    
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseIdentityServer();
}

// using AuthenticationContext dbContext =
//     app.Services.CreateScope().ServiceProvider.GetRequiredService<AuthenticationContext>();
// dbContext.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

