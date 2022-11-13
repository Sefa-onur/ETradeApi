using ETradeApi.API.Extensions;
using ETradeApi.Application;
using ETradeApi.Application.Validators.Product;
using ETradeApi.Infrastructure;
using ETradeApi.Infrastructure.Filters;
using ETradeApi.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using SignalR;
using System.Collections.ObjectModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<ValidationsFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidators>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistanceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddSignalRService();


var columnOptions = new ColumnOptions
{
    AdditionalColumns = new Collection<SqlColumn>
    {
        new SqlColumn("message",System.Data.SqlDbType.NVarChar),
        new SqlColumn("message_template",System.Data.SqlDbType.NVarChar),
        new SqlColumn("level",System.Data.SqlDbType.NVarChar),
        new SqlColumn("time_stamp",System.Data.SqlDbType.NVarChar),
        new SqlColumn("exeptions",System.Data.SqlDbType.NVarChar),
        new SqlColumn("log_events",System.Data.SqlDbType.NVarChar),
    }
};
Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log/log.txt")
    .WriteTo.MSSqlServer("Data Source=SEFAONUR\\SQLEXPRESS;Initial Catalog=ETradeApi;User ID=sa;Password=3394320,TrustServerCertificate=True", sinkOptions: new MSSqlServerSinkOptions
    {
        TableName = "log",
        AutoCreateSqlTable = true
    }, null, null, LogEventLevel.Information, null, columnOptions: columnOptions)
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Token:Audience"],
            ValidAudience = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExeptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHups();

app.Run();
