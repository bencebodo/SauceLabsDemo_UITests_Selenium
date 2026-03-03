using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemo_Core.Context;
using SauceDemo_Core.Services.UIServices;
using SauceDemo_Pages.PageObjects;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace SauceDemo_UI_Tests
{
    public static class Dependencies
    {
        public static IServiceProvider ConfigureServices() {             
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
            services.AddScoped<IWebDriver>(provider =>
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--incognito");
                options.AddArgument("--headless");
                return new ChromeDriver(options);
            });
            services.AddScoped<LoginPage>();
            services.AddScoped<UIAuthenticationService>();
            services.AddScoped<ProductsPage>();
            services.AddScoped<CartPage>();
            services.AddScoped<CheckoutPage>();
            services.AddScoped<OverviewPage>();
            services.AddScoped<ThankYouPage>();
            services.AddScoped<UIProductsService>();
            services.AddScoped<UICartService>();
            services.AddScoped<UICheckoutService>();
            return services.BuildServiceProvider();
        }
    }
}
