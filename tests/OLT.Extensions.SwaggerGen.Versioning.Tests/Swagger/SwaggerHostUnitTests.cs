using Microsoft.AspNetCore.TestHost;
using Microsoft.OpenApi;
using OLT.Extensions.SwaggerGen.Versioning.Tests.Assets;
using OLT.Extensions.SwaggerGen.Versioning.Tests.Controllers.Models;
using System.Net;

namespace OLT.Extensions.SwaggerGen.Versioning.Tests.Swagger;

public class SwaggerHostUnitTests : BaseSwaggerUnitTests
{
    //const string version1 = "v1"; //Completely Deprecated 
    //const string version2 = "v2"; //Partially Deprecated
    //const string version3 = "v3"; //Nothing Deprecated

    private const string DefaultTitle = "API";
    private const string DefaultDescription = "Api Methods";


    [Theory]
    [InlineData("v1", true)]
    [InlineData("v2", false)]
    [InlineData("v3", false)]
    public async Task Test_SwaggerJson_Contains_Expected_Values_With_Security_Schemes(string version, bool completelyDeprecated)
    {

        var testParams = new GeneralTestParams(Faker.Company.Name(), Faker.Lorem.Sentence(), true, true)
        {
            Contact = new OpenApiContact { Name = Faker.Name.FullName(), Url = new System.Uri(Faker.Internet.Url()), Email = Faker.Internet.Email() },
            License = new OpenApiLicense { Name = Faker.Lorem.GetFirstWord(), Url = new System.Uri(Faker.Internet.Url()) },
        };

        var args = new OltSwaggerArgs(new OltOptionsApiVersion())
            .WithTitle(testParams.Title)
            .WithDescription(testParams.Description)
            .WithSecurityScheme(new OltSwaggerJwtBearerToken())
            .WithSecurityScheme(new OltSwaggerApiKey())
            .WithOperationFilter(new OltDefaultValueFilter())
            .WithApiContact(testParams.Contact)
            .WithApiLicense(testParams.License)
            .Enable(true);

        
        using (var testServer = new TestServer(TestHostBuilder.WebHostBuilder<HostStartupNew>(args)))
        {
            await GeneralTests(testServer, version, testParams);
            var jsonString = await GetSwaggerJson(testServer, version);
            Assert.Contains(testParams.Title, jsonString);
            Assert.Contains(testParams.GetExpectedDescription(completelyDeprecated), jsonString);
            Assert.Contains("X-API-KEY", jsonString);
            Assert.Contains("JWT", jsonString);
            Assert.Contains("Bearer", jsonString);
            Assert.Contains("api-version", jsonString);
            Assert.Contains("The requested API version", jsonString);

            if (!completelyDeprecated)
            {
                Assert.DoesNotContain(testParams.BuildDeprecatedDescription()!, jsonString);
            }          
        }
    }

    [Theory]
    [InlineData("v1", true)]
    [InlineData("v2", false)]
    [InlineData("v3", false)]
    public async Task Test_SwaggerJson_Contains_Expected_Values_Without_Security_Schemes(string version, bool completelyDeprecated)
    {

        var testParams = new GeneralTestParams(Faker.Company.Name(), Faker.Lorem.Sentence(), false, true);

        var args = new OltSwaggerArgs(new OltOptionsApiVersion())
            .WithTitle(testParams.Title)
            .WithDescription(testParams.Description)
            .WithOperationFilter(new OltDefaultValueFilter())
            .Enable(true);


        using (var testServer = new TestServer(TestHostBuilder.WebHostBuilder<HostStartupNew>(args)))
        {
            await GeneralTests(testServer, version, testParams);
            var jsonString = await GetSwaggerJson(testServer, version);            
            Assert.Contains(testParams.Title, jsonString);
            Assert.Contains(testParams.GetExpectedDescription(completelyDeprecated), jsonString);
            Assert.DoesNotContain("X-API-KEY", jsonString);
            Assert.DoesNotContain("JWT", jsonString);
            Assert.DoesNotContain("Bearer", jsonString);
            Assert.Contains("api-version", jsonString);
            Assert.Contains("The requested API version", jsonString);
        }
    }


    [Theory]
    [InlineData("v1", true)]
    [InlineData("v2", false)]
    [InlineData("v3", false)]
    public async Task Test_SwaggerJson_With_Null_Title_And_Description(string version, bool completelyDeprecated)
    {
        var testParams = new GeneralTestParams(DefaultTitle, DefaultDescription, false, true);

        var args = new OltSwaggerArgs(new OltOptionsApiVersion())
            .WithTitle(null)
            .WithDescription(null)
            .WithXmlComments(Faker.Internet.UserName())
            .Enable(true);


        using (var testServer = new TestServer(TestHostBuilder.WebHostBuilder<HostStartupNew>(args)))
        {
            await GeneralTests(testServer, version, testParams);
            var jsonString = await GetSwaggerJson(testServer, version);
            Assert.Contains(testParams.Title, jsonString);
            Assert.Contains(testParams.GetExpectedDescription(completelyDeprecated), jsonString);
            Assert.DoesNotContain("X-API-KEY", jsonString);
            Assert.DoesNotContain("JWT", jsonString);
            Assert.DoesNotContain("Bearer", jsonString);
            Assert.Contains("api-version", jsonString);
            Assert.DoesNotContain("The requested API version", jsonString);
        }
    }


    [Theory]
    [InlineData("v1", true)]
    [InlineData("v2", false)]
    [InlineData("v3", false)]
    public async Task Test_SwaggerJson_With_Default_Title_And_Description(string version, bool completelyDeprecated)
    {
        var testParams = new GeneralTestParams(DefaultTitle, DefaultDescription, false, true);

        var args = new OltSwaggerArgs(new OltOptionsApiVersion())
            .WithXmlComments(Faker.Internet.UserName())
            .Enable(true);


        using (var testServer = new TestServer(TestHostBuilder.WebHostBuilder<HostStartupNew>(args)))
        {
            await GeneralTests(testServer, version, testParams);
            var jsonString = await GetSwaggerJson(testServer, version);
            Assert.Contains(testParams.Title, jsonString);
            Assert.Contains(testParams.GetExpectedDescription(completelyDeprecated), jsonString);
            Assert.DoesNotContain("X-API-KEY", jsonString);
            Assert.DoesNotContain("JWT", jsonString);
            Assert.DoesNotContain("Bearer", jsonString);
            Assert.Contains("api-version", jsonString);
            Assert.DoesNotContain("The requested API version", jsonString);
        }
    }

    [Theory]
    [InlineData("v1")]
    [InlineData("v2")]
    [InlineData("v3")]
    public async Task Test_Swagger_Endpoint_Disabled(string version)
    {
        var testParams = new GeneralTestParams(DefaultTitle, DefaultDescription, false, true);


        HostStartup.Args = new OltSwaggerArgs(new OltOptionsApiVersion())
            .WithTitle(testParams.Title)
            .WithDescription(testParams.Description)
            .WithOperationFilter(new OltDefaultValueFilter())
            .Enable(false);

        var builder = TestHostBuilder.WebHostBuilder<HostStartup>();

        using (var testServer = new TestServer(builder))
        {
            var response = await testServer.CreateRequest($"/swagger/{version}/swagger.json").SendAsync("GET");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    [Theory]
    [InlineData("v1")]
    //[InlineData("v2")]
    //[InlineData("v3")]
    public async Task Test_SwaggerJson_With_CamelCase_Enabled(string version)
    {
        HostStartup.Args = new OltSwaggerArgs(new OltOptionsApiVersion())
            .WithTitle(Faker.Company.Name())
            .WithDescription(Faker.Lorem.Sentence())
            .WithOperationFilter(new OltDefaultValueFilter())
            .WithOperationFilter(new OltCamelCasingOperationFilter())
            .Enable(true);

        var builder = TestHostBuilder.WebHostBuilder<HostStartup>();

        using (var testServer = new TestServer(builder))
        {
            var jsonString = await GetSwaggerJson(testServer, version);
            Assert.DoesNotContain("\"name\": \"RouteId\"", jsonString);
            Assert.Contains("\"name\": \"routeId\"", jsonString);
        }

    }

    [Theory]
    [InlineData("v1")]
    [InlineData("v2")]
    [InlineData("v3")]
    public async Task Test_SwaggerJson_With_CamelCase_Disabled(string version)
    {
        HostStartup.Args = new OltSwaggerArgs(new OltOptionsApiVersion())
            .WithTitle(Faker.Company.Name())
            .WithDescription(Faker.Lorem.Sentence())
            .WithOperationFilter(new OltDefaultValueFilter())
            .Enable(true);

        var builder = TestHostBuilder.WebHostBuilder<HostStartup>();

        using (var testServer = new TestServer(builder))
        {
            var jsonString = await GetSwaggerJson(testServer, version);
            Assert.Contains("\"name\": \"RouteId\"", jsonString);
            Assert.DoesNotContain("\"name\": \"routeId\"", jsonString);
        }

    }

}
