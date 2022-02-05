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

        [Fact]
        public async Task SwaggerTests()
        {
            //Due to reusing TestStartup, I call each test in sequence 
            await Test1();
            await Test2();
            await Test3();
            await Test4();
        }

        public async Task Test1()
        {
            TestStartup.Title = Faker.Company.Name();
            TestStartup.Description = Faker.Lorem.Sentence();
            TestStartup.Contact = new OpenApiContact { Name = Faker.Name.FullName(), Url = new System.Uri(Faker.Internet.Url()), Email = Faker.Internet.Email() };
            TestStartup.License = new OpenApiLicense { Name = Faker.Lorem.GetFirstWord(), Url = new System.Uri(Faker.Internet.Url()) };

            var expected = new GeneralTestExpect(TestStartup.Title, TestStartup.Description, true, true)
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
                await GeneralTests(testServer, expected);

                using (var client = testServer.CreateClient())
                {
                    string result = await client.GetStringAsync("/swagger/v1/swagger.json");
                    JObject root = JObject.Parse(result);
                    Assert.NotNull(root);
                    var jsonString = root.ToString();
                    Assert.Contains(TestStartup.Title, jsonString);
                    Assert.Contains(TestStartup.Description, jsonString);
                    Assert.Contains("X-API-KEY", jsonString);
                    Assert.Contains("JWT", jsonString);
                    Assert.Contains("Bearer", jsonString);
                    Assert.Contains("api-version", jsonString);
                    Assert.Contains("The requested API version", jsonString);
                }

            }
        }

        public async Task Test2()
        {
            TestStartup.Title = Faker.Company.Name();
            TestStartup.Description = Faker.Lorem.Sentence();
            TestStartup.Contact = null;
            TestStartup.License = null;


            TestStartup.Args = new OltSwaggerArgs()
                .WithTitle(TestStartup.Title)
                .WithDescription(TestStartup.Description)
                .WithOperationFilter(new OltDefaultValueFilter())
                .Enable(true);

            var builder = TestExtensions.WebHostBuilder<TestStartup>();

            using (var testServer = new TestServer(builder))
            {
                await GeneralTests(testServer, new GeneralTestExpect(TestStartup.Title, TestStartup.Description, false, true));

                using (var client = testServer.CreateClient())
                {
                    string result = await client.GetStringAsync("/swagger/v1/swagger.json");
                    JObject root = JObject.Parse(result);
                    Assert.NotNull(root);
                    var jsonString = root.ToString();
                    Assert.Contains(TestStartup.Title, jsonString);
                    Assert.Contains(TestStartup.Description, jsonString);
                    Assert.DoesNotContain("X-API-KEY", jsonString);
                    Assert.DoesNotContain("JWT", jsonString);
                    Assert.DoesNotContain("Bearer", jsonString);
                    Assert.Contains("api-version", jsonString);
                    Assert.Contains("The requested API version", jsonString);
                }


            }


        }

        public async Task Test3()
        {
            TestStartup.Title = null;
            TestStartup.Description = null;
            TestStartup.Contact = null;
            TestStartup.License = null;

            var product = Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product;
            var description = Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyDescriptionAttribute>()?.Description;

            TestStartup.Args = new OltSwaggerArgs()
                .WithXmlComments(Faker.Internet.UserName())
                .Enable(true);

            var builder = TestExtensions.WebHostBuilder<TestStartup>();

            using (var testServer = new TestServer(builder))
            {

                await GeneralTests(testServer, new GeneralTestExpect(product, description, false, true));

                using (var client = testServer.CreateClient())
                {
                    string result = await client.GetStringAsync("/swagger/v1/swagger.json");
                    JObject root = JObject.Parse(result);
                    Assert.NotNull(root);
                    var jsonString = root.ToString();
                    Assert.Contains(product, jsonString);
                    Assert.Contains(description, jsonString);
                    Assert.DoesNotContain("X-API-KEY", jsonString);
                    Assert.DoesNotContain("JWT", jsonString);
                    Assert.DoesNotContain("Bearer", jsonString);
                    Assert.Contains("api-version", jsonString);
                    Assert.DoesNotContain("The requested API version", jsonString);
                }


            }


        }

        public async Task Test4()
        {
            TestStartup.Title = null;
            TestStartup.Description = null;
            TestStartup.Contact = null;
            TestStartup.License = null;

            var product = Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product;
            var description = Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyDescriptionAttribute>()?.Description;


            TestStartup.Args = new OltSwaggerArgs()
                .WithXmlComments(Faker.Internet.UserName())
                .Enable(true);

            var builder = TestExtensions.WebHostBuilder<TestStartup>();

            using (var testServer = new TestServer(builder))
            {
                await GeneralTests(testServer, new GeneralTestExpect(product, description, false, true));

                using (var client = testServer.CreateClient())
                {
                    string result = await client.GetStringAsync("/swagger/v1/swagger.json");
                    JObject root = JObject.Parse(result);
                    Assert.NotNull(root);
                    var jsonString = root.ToString();
                    Assert.Contains(product, jsonString);
                    Assert.Contains(description, jsonString);
                    Assert.DoesNotContain("X-API-KEY", jsonString);
                    Assert.DoesNotContain("JWT", jsonString);
                    Assert.DoesNotContain("Bearer", jsonString);
                    Assert.Contains("api-version", jsonString);
                    Assert.DoesNotContain("The requested API version", jsonString);
                }


            }


        }

        private async Task GeneralTests(TestServer testServer, GeneralTestExpect expected)
        {
            var swaggerProvider = testServer.Services.GetRequiredService<ISwaggerProvider>();

            swaggerProvider.Should().NotBeNull();

            var swagger = swaggerProvider.GetSwagger("v1");

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
                       

            var response = await testServer.CreateRequest("/swagger/v1/swagger.json").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}