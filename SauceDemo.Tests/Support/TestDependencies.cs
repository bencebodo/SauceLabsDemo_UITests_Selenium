using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SauceDemo.Core.Drivers;
using SauceDemo.Core.Services.UIServices;
using SauceDemo.Pages.PageObjects;
using SauceDemo.Tests.Context;
using Serilog;

namespace SauceDemo.Tests.Support
{
    public static class TestDependencies
    {
        public static IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            var serilogLogger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(serilogLogger, dispose: true);
            });
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<IDriverFactory, DriverFactory>();
            services.AddScoped<UIContext>();
            services.AddScoped<IWebDriver>(provider =>
            provider.GetRequiredService<UIContext>().Driver);
            var pagesAssembly = typeof(LoginPage).Assembly;
            var pageTypes = pagesAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Page"));

            foreach (var type in pageTypes)
            {
                services.AddScoped(type);
            }
            services.AddScoped<UIAuthenticationService>();
            services.AddScoped<UICartService>();
            services.AddScoped<UIProductsService>();
            services.AddScoped<UICheckoutService>();
            return services.BuildServiceProvider();
        }
    }
}
