using Infrastructure;
using Infrastructure.Hosting;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using ServiceCollector;
using WebService.Middlewares;

var options = new MyWebApiPipelineBuilderOptions
{
    Args = args,

    // Service-specific DI
    ConfigureServices = (services, config) =>
    {
        services.AddDbContext<AppDbContext>(o =>
            o.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddProductCommandServices(); // custom DI
    },

    // Service-specific middleware
    ConfigureMiddlewares = (app, config) =>
    {
        var forwardOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        };
        forwardOptions.KnownNetworks.Clear();
        forwardOptions.KnownProxies.Clear();
        app.UseForwardedHeaders(forwardOptions);

        app.UseMiddleware<DynamicRateLimitMiddleware>();
    }
};

// Build & run using centralized hosting
var app = MyWebApiPipelineBuilder.Build(options);
app.Run();
