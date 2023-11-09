using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders;
using Serilog;
using AutoMapper;
using CashFlow.Data.DbContexts;
using CashFlow.Api.MiddleWare;
using CashFlow.Service.Helpers;
using System.IO;
using CashFlow.Service.Services;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Shamsheer.Messenger.Api.Models;
using CashFlow.Data;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
// Add DbContext
builder.Services.AddDbContext<CashFlowDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();



// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AAA", builder => builder.WithOrigins("https://82.215.96.174").AllowAnyHeader().AllowAnyMethod());
});
//Jwt
//builder.Services.AddJwtService(builder.Configuration);

//Swagger
builder.Services.AddSwaggerService();

builder.Services.AddCustomService();
// Add AutoMapper

builder.Services.AddAutoMapper(typeof(MappingProfile));

//Configure api url name
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(
                                        new ConfigurationApiUrlName()));
});

// Set up the logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

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
// Use custom middleware
app.UseMiddleware<ExceptionHendlerMiddleWare>();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AAA");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
