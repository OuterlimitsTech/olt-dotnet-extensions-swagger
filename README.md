[![CI](https://github.com/OuterlimitsTech/olt-dotnet-extensions-swagger/actions/workflows/build.yml/badge.svg)](https://github.com/OuterlimitsTech/olt-dotnet-extensions-swagger/actions/workflows/build.yml) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=OuterlimitsTech_olt-dotnet-extensions-swagger&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=OuterlimitsTech_olt-dotnet-extensions-swagger) [![Nuget](https://img.shields.io/nuget/v/OLT.Extensions.SwaggerGen.Versioning)](https://www.nuget.org/packages/OLT.Extensions.SwaggerGen.Versioning)

## Builder for Swagger with Api Versioning

Good medium article
https://medium.com/c-sharp-progarmming/xml-comments-swagger-net-core-a390942d3329

### NOTE!!!

1. This package was renamed from **OLT.Extensions.SwaggerGen** to **OLT.Extensions.SwaggerGen.Versioning** due to combining it with **OLT.AspNetCore.Versioning**

2. The namespace shifted from **OLT.Extensions.SwaggerGen** to **OLT.Extensions.SwaggerGen.Versioning**

## Example Code

```csharp
public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        Configuration = configuration;
        HostEnvironment = hostEnvironment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment HostEnvironment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
       var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

       var enableSwagger = System.Diagnostics.Debugger.IsAttached || HostEnvironment.IsDevelopment();

       ...
       services.AddSwaggerWithVersioning(
          new OltSwaggerArgs(new OltOptionsApiVersion())
                .WithTitle(Title)
                .WithDescription(Description)
                .WithSecurityScheme(new OltSwaggerJwtBearerToken())  // Allow JWT Token to be passed via the Swagger UI
                .WithSecurityScheme(new OltSwaggerApiKey())    // Allow X-API-KEY to be passed via the Swagger UI
                .WithOperationFilter(new OltDefaultValueFilter())
                .WithApiContact(new Microsoft.OpenApi.Models.OpenApiContact { Name = "John Doe", Url = new System.Uri("https://www.nuget.org/"), Email = "john.doe@fake-email.com" })
                .WithApiLicense(new Microsoft.OpenApi.Models.OpenApiLicense { Name = "License Here", Url = new System.Uri("https://www.google.com/") })
                .WithXmlComments(xmlPath)  //Enabling XML comments is required for this to function
                .Enable(enableSwagger);

       ...
    }


    public void Configure(IApplicationBuilder app)
    {
        ...
        app.UseSwaggerWithVersioning();
        ...
    }
}
```
