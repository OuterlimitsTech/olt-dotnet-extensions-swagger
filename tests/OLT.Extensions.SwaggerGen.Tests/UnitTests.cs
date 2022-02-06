using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using Microsoft.OpenApi.Models;

namespace OLT.Extensions.SwaggerGen.Tests
{

    public class UnitTests
    {

        const string Deprecated = " - DEPRECATED";

        [Fact]
        public async Task SwaggerTests()
        {
            const string version1 = "v1"; //Completely Deprecated 
            const string version2 = "v2"; //Partially Deprecated
            const string version3 = "v3"; //Nothing Deprecated

            //Due to reusing TestStartup, I call each test in sequence             
            await Test1(version1, true);
            await Test2(version1, true);
            await Test3(version1, true);
            await Test4(version1, true);

            await Test1(version2, false);
            await Test2(version2, false);
            await Test3(version2, false);
            await Test4(version2, false);

            await Test1(version3, false);
            await Test2(version3, false);
            await Test3(version3, false);
            await Test4(version3, false);

        }


        public async Task Test1(string version, bool completelyDeprecated)
        {
            
            TestStartup.Title = Faker.Company.Name();
            TestStartup.Description = Faker.Lorem.Sentence();
            TestStartup.Contact = new OpenApiContact { Name = Faker.Name.FullName(), Url = new System.Uri(Faker.Internet.Url()), Email = Faker.Internet.Email() };
            TestStartup.License = new OpenApiLicense { Name = Faker.Lorem.GetFirstWord(), Url = new System.Uri(Faker.Internet.Url()) };

            var expectedDescription = completelyDeprecated ? $"{TestStartup.Description}{Deprecated}" : TestStartup.Description;            

            var expected = new GeneralTestParams(TestStartup.Title, expectedDescription, true, true)
            {
                Contact = TestStartup.Contact,
                License = TestStartup.License,
            };

            TestStartup.Args = new OltSwaggerArgs()
                .WithTitle(TestStartup.Title)
                .WithDescription(TestStartup.Description)
                .WithSecurityScheme(new OltSwaggerJwtBearerToken())
                .WithSecurityScheme(new OltSwaggerApiKey())
                .WithOperationFilter(new OltDefaultValueFilter())
                .WithApiContact(TestStartup.Contact)
                .WithApiLicense(TestStartup.License)
                .WithXmlComments()
                .Enable(true);

            var builder = TestExtensions.WebHostBuilder<TestStartup>();

            using (var testServer = new TestServer(builder))
            {
                await GeneralTests(testServer, version, expected);
                var jsonString = await GetSwaggerJson(testServer, version);
                Assert.Contains(TestStartup.Title, jsonString);
                Assert.Contains(expectedDescription, jsonString);
                Assert.Contains("X-API-KEY", jsonString);
                Assert.Contains("JWT", jsonString);
                Assert.Contains("Bearer", jsonString);
                Assert.Contains("api-version", jsonString);
                Assert.Contains("The requested API version", jsonString);

                if (!completelyDeprecated)
                {
                    Assert.DoesNotContain($"{TestStartup.Description}{Deprecated}", jsonString);
                }
            }
        }

        public async Task Test2(string version, bool completelyDeprecated)
        {
            TestStartup.Title = Faker.Company.Name();
            TestStartup.Description = Faker.Lorem.Sentence();
            TestStartup.Contact = null;
            TestStartup.License = null;

            var expectedDescription = completelyDeprecated ? $"{TestStartup.Description}{Deprecated}" : TestStartup.Description;

            TestStartup.Args = new OltSwaggerArgs()
                .WithTitle(TestStartup.Title)
                .WithDescription(TestStartup.Description)
                .WithOperationFilter(new OltDefaultValueFilter())
                .Enable(true);

            var builder = TestExtensions.WebHostBuilder<TestStartup>();

            using (var testServer = new TestServer(builder))
            {
                await GeneralTests(testServer, version, new GeneralTestParams(TestStartup.Title, expectedDescription, false, true));
                var jsonString = await GetSwaggerJson(testServer, version);
                Assert.Contains(TestStartup.Title, jsonString);
                Assert.Contains(expectedDescription, jsonString);
                Assert.DoesNotContain("X-API-KEY", jsonString);
                Assert.DoesNotContain("JWT", jsonString);
                Assert.DoesNotContain("Bearer", jsonString);
                Assert.Contains("api-version", jsonString);
                Assert.Contains("The requested API version", jsonString);
            }


        }

        public async Task Test3(string version, bool completelyDeprecated)
        {
            TestStartup.Title = null;
            TestStartup.Description = null;
            TestStartup.Contact = null;
            TestStartup.License = null;

            var product = Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product;
            var description = Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyDescriptionAttribute>()?.Description;

            var expectedDescription = completelyDeprecated ? $"{description}{Deprecated}" : description;

            TestStartup.Args = new OltSwaggerArgs()
                .WithXmlComments(Faker.Internet.UserName())
                .Enable(true);

            var builder = TestExtensions.WebHostBuilder<TestStartup>();

            using (var testServer = new TestServer(builder))
            {

                await GeneralTests(testServer, version, new GeneralTestParams(product, expectedDescription, false, true));
                var jsonString = await GetSwaggerJson(testServer, version);
                Assert.Contains(product, jsonString);
                Assert.Contains(expectedDescription, jsonString);
                Assert.DoesNotContain("X-API-KEY", jsonString);
                Assert.DoesNotContain("JWT", jsonString);
                Assert.DoesNotContain("Bearer", jsonString);
                Assert.Contains("api-version", jsonString);
                Assert.DoesNotContain("The requested API version", jsonString);
            }


        }

        public async Task Test4(string version, bool completelyDeprecated)
        {
            TestStartup.Title = null;
            TestStartup.Description = null;
            TestStartup.Contact = null;
            TestStartup.License = null;

            var product = Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product;
            var description = Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyDescriptionAttribute>()?.Description;

            var expectedDescription = completelyDeprecated ? $"{description}{Deprecated}" : description;

            TestStartup.Args = new OltSwaggerArgs()
                .WithXmlComments(Faker.Internet.UserName())
                .Enable(true);

            var builder = TestExtensions.WebHostBuilder<TestStartup>();

            using (var testServer = new TestServer(builder))
            {
                await GeneralTests(testServer, version, new GeneralTestParams(product, expectedDescription, false, true));
                var jsonString = await GetSwaggerJson(testServer, version);
                Assert.Contains(product, jsonString);
                Assert.Contains(expectedDescription, jsonString);
                Assert.DoesNotContain("X-API-KEY", jsonString);
                Assert.DoesNotContain("JWT", jsonString);
                Assert.DoesNotContain("Bearer", jsonString);
                Assert.Contains("api-version", jsonString);
                Assert.DoesNotContain("The requested API version", jsonString);
            }


        }

        private static async Task<string> GetSwaggerJson(TestServer testServer, string version)
        {
            using (var client = testServer.CreateClient())
            {
                string result = await client.GetStringAsync($"/swagger/{version}/swagger.json");
                JObject root = JObject.Parse(result);
                Assert.NotNull(root);
                return root.ToString();                
            }
        }

        private static async Task GeneralTests(TestServer testServer, string version, GeneralTestParams expected)
        {
            var swaggerProvider = testServer.Services.GetRequiredService<ISwaggerProvider>();

            swaggerProvider.Should().NotBeNull();

            var swagger = swaggerProvider.GetSwagger(version);

            swagger.Should().NotBeNull();
            swagger.SecurityRequirements.Any().Should().Be(expected.HasSecurityReq);
            swagger.Paths.Any().Should().Be(expected.HasPaths);
            swagger.Components.Schemas.Should().NotBeNull();
            Assert.Equal(expected.Title, swagger.Info.Title);
            Assert.Equal(expected.Description, swagger.Info.Description);
            if (expected.Contact == null)
            {
                swagger.Info.Contact.Should().BeNull();
            }
            else
            {
                swagger.Info.Contact.Should().BeEquivalentTo(expected.Contact);
            }

            if (expected.License == null)
            {
                swagger.Info.License.Should().BeNull();
            }
            else
            {
                swagger.Info.License.Should().BeEquivalentTo(expected.License);
            }
                       

            var response = await testServer.CreateRequest($"/swagger/{version}/swagger.json").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}