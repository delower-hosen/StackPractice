using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Hosting;

public class MyWebApiPipelineBuilderOptions
{
    public string[] Args { get; set; } = [];
    public bool EnableSwagger { get; set; } = true;

    public Action<IServiceCollection, IConfiguration>? ConfigureServices { get; set; }

    public Action<WebApplication, IConfiguration>? ConfigureMiddlewares { get; set; }

    public Action<WebApplication>? ConfigureRoutes { get; set; }
}
