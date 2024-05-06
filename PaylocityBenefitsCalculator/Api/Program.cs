using Api;
using Api.Domain.Configs;
using Api.Domain.Paychecks;
using Api.Domain.Rules;
using Api.Domain.Segmenting;
using Api.Domain.Tools;
using Api.Domain.Validations;
using Api.Middleware;
using Api.Services;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// services
builder.Services.AddSingleton<IEmployeesDao, EmployeesDao>();

// domain - paychecks
builder.Services.AddSingleton<IPaycheckService, PaycheckService>();
builder.Services.AddSingleton<IPaycheckCalculator, PaycheckCalculator>();

// domain - segmenting
// TODO: would be nice a factory here and choose segmenter based on configuration
//builder.Services.AddSingleton<IPayPeriodSegmenter, TwoWeeksPayPeriodSegmenter>();
builder.Services.AddSingleton<IPayPeriodSegmenter, FullYearPayPeriodSegmenter>();

// domain - rules
builder.Services.AddSingleton<IRule, SalaryRule>();
builder.Services.AddSingleton<IRule, BaseCostRule>();
builder.Services.AddSingleton<IRule, DependentCostRule>();
builder.Services.AddSingleton<IRule, HighSalaryCostRule>();
builder.Services.AddSingleton<IRule, FiftyYearsDependentRule>();

// domain - tools, config, validations
builder.Services.AddSingleton<IConfig, Config>();
builder.Services.AddSingleton<ICalendarTools, CalendarTools>();
builder.Services.AddSingleton<IDaysCalculator, DaysCalculator>();
builder.Services.AddSingleton<IRatesCalculator, RatesCalculator>();
builder.Services.AddSingleton<IValidation, EmployeeCanHaveOnlyOnePartnerValidation>();

// mapper
builder.Services.AddAutoMapper(typeof(DtoProfile));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Calculation Api",
        Description = "Api to support employee benefit cost calculations"
    });
});

var allowLocalhost = "allow localhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowLocalhost,
        policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowLocalhost);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseErrorHandlingMiddleware();

app.Run();

// enables TestHost
public partial class Program { }
