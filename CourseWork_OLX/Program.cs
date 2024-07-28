using BusinessLogic.Exstensions;
using CourseWork_OLX.Extensions;
using CourseWork_OLX.Middlewares;
using DataAccess;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddOlxDbContext(connStr);
builder.Services.AddBusinessLogicServices();
// Add services to the container.
builder.Services.AddMainServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors("AllowOrigins");

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    serviceProvider.SeedData(builder.Configuration).Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
