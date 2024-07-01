using FluentValidation.AspNetCore;
using TaxCalculator.Infrastructure.Interfaces;
using TaxCalculator.Infrastructure.Repositories;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.Services.Mapping;
using TaxCalculator.Services.Settings;
using TaxCalculator.Services.Validations;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ITaxService, TaxService>();
builder.Services.AddSingleton<IMappingService, MappingService>();
builder.Services.Configure<TaxRulesSettings>(builder.Configuration.GetSection("TaxRules"));
builder.Services.AddScoped<ITaxPayerRepository, TaxPayerRepository>();
builder.Logging.AddConsole();

builder.Services.AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<TaxPayerDTOValidator>());



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
