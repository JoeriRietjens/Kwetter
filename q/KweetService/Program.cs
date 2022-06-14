using kwetter_backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var policy = "policy";
//Frontend runs on https://localhost:7185;http://localhost:5153
builder.Services.AddCors(options =>
{
    options.AddPolicy(policy, 
        policy  =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("CorsPolicy",
//         builder =>
//         {
//             builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
//         });
// });

string connectionString = builder.Configuration.GetConnectionString("postgres");

builder.Services.AddDbContext<KweetContext>(options =>
{
    //options.UseNpgsql(connectionString);
    options.UseInMemoryDatabase("kwetter");
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

using KweetContext dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<KweetContext>();

dbContext.Database.EnsureCreated();

//app.UseHttpsRedirection(); FUCKING COMMENT IT OUT!!

app.UseRouting();

app.UseCors("policy");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();