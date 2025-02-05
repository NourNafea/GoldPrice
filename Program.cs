using GoldPrice.Models;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Add SendGrid configuration
builder.Services.AddHttpClient();
builder.Services.Configure<SendGridOptions>(builder.Configuration.GetSection("SendGrid"));

// Add Durable Functions
// Functionsbuilder.Services.AddDurableTaskClient();

// Application Insights for monitoring (recommended for production)
// builder.Services
//     // .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();