using CashFlow.Api.Extensions;
using CashFlow.Api.MiddleWare;
using CashFlow.Data.DbContexts;
using CashFlow.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CashFlowDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomService();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext().CreateLogger();

builder.Logging.ClearProviders(); builder.Logging.AddSerilog(logger);

var app = builder.Build();

WebHostEnvironmentHelper.WebRootPath = Path.GetFullPath("wwwroot");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name");
    });
}


app.UseMiddleware<ExceptionHendlerMiddleWare>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
