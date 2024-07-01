using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Infrastructure.Interfaces;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.Services.Mapping;
using TaxCalculator.Services.Settings;

namespace TaxCalculator.Tests
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TaxRulesSettings>(Configuration.GetSection("TaxRules"));

            services.AddScoped<ITaxService, TaxService>();
            services.AddSingleton<IMappingService, MappingService>();

            var mockRepository = new Mock<ITaxPayerRepository>();
            services.AddTransient<ITaxPayerRepository>(provider => mockRepository.Object);
        }

    }
}