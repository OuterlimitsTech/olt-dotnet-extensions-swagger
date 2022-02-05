[![CI](https://github.com/OuterlimitsTech/olt-dotnet-extensions-configuration-flagsmith/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/OuterlimitsTech/olt-dotnet-extensions-configuration-flagsmith/actions/workflows/build.yml) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=OuterlimitsTech_olt-dotnet-extensions-swagger&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=OuterlimitsTech_olt-dotnet-extensions-swagger) [![Nuget](https://img.shields.io/nuget/v/OLT.Extensions.SwaggerGen)](https://www.nuget.org/packages/OLT.Extensions.SwaggerGen)

## Builder for Swagger with Api Versioning

```csharp

public void ConfigureServices(IServiceCollection services)
{
   ...
   services.AddSwaggerWithVersioning(
      new OltSwaggerArgs()
            .WithTitle(Title)
            .WithDescription(Description)
            .WithSecurityScheme(new OltSwaggerJwtBearerToken())
            .WithSecurityScheme(new OltSwaggerApiKey())
            .WithOperationFilter(new OltDefaultValueFilter())
            .WithApiContact(new OpenApiContact { Name = Faker.Name.FullName(), Url = new System.Uri(Faker.Internet.Url()), Email = Faker.Internet.Email() })
            .WithApiLicense(new OpenApiLicense { Name = Faker.Lorem.GetFirstWord(), Url = new System.Uri(Faker.Internet.Url()) })
            .WithXmlComments()
            .Enable(true);

   ...
}

```

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  ...
  app.UseSwaggerWithVersioning();
  ...
}

```
