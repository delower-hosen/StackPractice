using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Hosting;

public static class MyWebApiPipelineBuilder
{
    public static WebApplication Build(MyWebApiPipelineBuilderOptions options)
    {
        var builder = WebApplication.CreateBuilder(options.Args);

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();

        builder.Configuration
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        if (options.EnableSwagger)
            builder.Services.AddSwaggerGen();

        options.ConfigureServices?.Invoke(builder.Services, builder.Configuration);

        var app = builder.Build();

        if (options.EnableSwagger && (app.Environment.IsDevelopment() || builder.Configuration.GetValue<bool>("EnableSwaggerInProd")))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        options.ConfigureMiddlewares?.Invoke(app, builder.Configuration);

        app.MapControllers();

        options.ConfigureRoutes?.Invoke(app);

        return app;
    }
}