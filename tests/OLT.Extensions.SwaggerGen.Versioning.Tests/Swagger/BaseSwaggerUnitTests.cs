using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using OLT.Extensions.SwaggerGen.Versioning.Tests.Controllers.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Net;

namespace OLT.Extensions.SwaggerGen.Versioning.Tests.Swagger;

public abstract class BaseSwaggerUnitTests
{
    


    protected static async Task GeneralTests(TestServer testServer, string version, GeneralTestParams testParams)
    {
        var swaggerProvider = testServer.Services.GetRequiredService<ISwaggerProvider>();

        swaggerProvider.Should().NotBeNull();

        try
        {
            var swagger = swaggerProvider.GetSwagger(version);
            swagger.Should().NotBeNull();
            swagger.SecurityRequirements.Any().Should().Be(testParams.HasSecurityReq);
            swagger.Paths.Any().Should().Be(testParams.HasPaths);
            swagger.Components.Schemas.Should().NotBeNull();
            Assert.Equal(testParams.Title, swagger.Info.Title);
            Assert.Equal(testParams.Description, swagger.Info.Description);
            if (testParams.Contact == null)
            {
                swagger.Info.Contact.Should().BeNull();
            }
            else
            {
                swagger.Info.Contact.Should().BeEquivalentTo(testParams.Contact);
            }

            if (testParams.License == null)
            {
                swagger.Info.License.Should().BeNull();
            }
            else
            {
                swagger.Info.License.Should().BeEquivalentTo(testParams.License);
            }

        }
        catch (Exception ex)
        {
            var test = ex;
        }

        var response = await testServer.CreateRequest($"/swagger/{version}/swagger.json").SendAsync("GET");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    protected static async Task<string> GetSwaggerJson(TestServer testServer, string version)
    {
        using (var client = testServer.CreateClient())
        {
            string result = await client.GetStringAsync($"/swagger/{version}/swagger.json");
            JObject root = JObject.Parse(result);
            Assert.NotNull(root);
            return root.ToString();
        }
    }

}
