using FICR.Cooperation.Humanism.Data;
using FICR.Cooperation.Humanism.Data.Context;
using FICR.Cooperation.Humanism.Data.Contracts;
using FICR.Cooperation.Humanism.Data.Contracts.Base;
using FICR.Cooperation.Humanism.Data.Repository;
using FICR.Cooperation.Humanism.Services;
using FICR.Cooperation.Humanism.Services.Contracts;
using FICR.Cooperation.Humanism.Services.Decorator;
using FICR.Cooperation.Humanism.Services.Facebook;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CooperationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CooperationSqlServer")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IEventService, EventServices>();
builder.Services.AddHttpClient<FacebookApiService>();

// Configure LoggingApiServiceDecorator
builder.Services.AddTransient<IApiService>(provider =>
{
    var facebookService = provider.GetRequiredService<FacebookApiService>();
    var logger = provider.GetRequiredService<ILogger<LoggingApiServiceDecorator>>();
    return new LoggingApiServiceDecorator(facebookService, logger);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.Run();
