[![CI](https://github.com/OuterlimitsTech/olt-dotnet-extensions-swagger/actions/workflows/build.yml/badge.svg)](https://github.com/OuterlimitsTech/olt-dotnet-extensions-swagger/actions/workflows/build.yml) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=OuterlimitsTech_olt-dotnet-extensions-swagger&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=OuterlimitsTech_olt-dotnet-extensions-swagger) [![Nuget](https://img.shields.io/nuget/v/OLT.Extensions.SwaggerGen)](https://www.nuget.org/packages/OLT.Extensions.SwaggerGen)

## Builder for Swagger with Api Versioning

Good medium article
https://medium.com/c-sharp-progarmming/xml-comments-swagger-net-core-a390942d3329

## Property Group from csproj file

```xml
<PropertyGroup>
  <DocumentationFile>bin\$(Configuration)\$(TargetFramework)\$(MSBuildProjectName).xml</DocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

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
       var enableSwagger = HostEnvironment.EnvironmentName.Equals("Development", StringComparison.OrdinalIgnoreCase);

#if DEBUG
       enableSwagger = true;
#endif

       ...
       services.AddSwaggerWithVersioning(
          new OltSwaggerArgs()
                .WithTitle(Title)
                .WithDescription(Description)
                .WithSecurityScheme(new OltSwaggerJwtBearerToken())
                .WithSecurityScheme(new OltSwaggerApiKey())
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
