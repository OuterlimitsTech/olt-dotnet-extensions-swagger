using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Extensions.SwaggerGen.Versioning.Tests.ApiVersion;

public class HostStartup
{
    public HostStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected IConfiguration Configuration { get; }


    public virtual void ConfigureServices(IServiceCollection services)
    {
        //OltServiceCollectionAspnetCoreExtensions.AddOltAspNetCore(services);
        services.AddApiVersioning(new OltOptionsApiVersion());
        services.AddRouting();
    }


    public virtual void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }

}
