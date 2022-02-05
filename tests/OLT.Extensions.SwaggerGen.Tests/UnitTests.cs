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

namespace OLT.Extensions.SwaggerGen.Tests
{

    public class UnitTests
    {
        [Fact]
        public async Task TestSwagger()
        {

            var builder = TestExtensions.WebHostBuilder<TestStartup>();

            using (var testServer = new TestServer(builder))
            {

                var swaggerProvider = testServer.Services.GetRequiredService<ISwaggerProvider>();

                swaggerProvider.Should().NotBeNull();

                var swagger = swaggerProvider.GetSwagger("v1");                

                swagger.Should().NotBeNull();
                swagger.SecurityRequirements.Any().Should().BeTrue();
                //swagger.Paths.Any().Should().BeTrue();
                swagger.Components.Schemas.Should().NotBeNull();
                Assert.Equal(TestStartup.Title, swagger.Info.Title);
                Assert.Equal(TestStartup.Description, swagger.Info.Description);

                var response = await testServer.CreateRequest("/swagger/v1/swagger.json").SendAsync("GET");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                using (var client = testServer.CreateClient())
                {
                    string result = await client.GetStringAsync("/swagger/v1/swagger.json");
                    JObject root = JObject.Parse(result);
                    Assert.NotNull(root);
                    var jsonString = root.ToString();
                    Assert.Contains(TestStartup.Title, jsonString);
                    Assert.Contains(TestStartup.Description, jsonString);
                }

            }

        }

        //[Fact]
        //public void Options()
        //{
        //    var services = new ServiceCollection();

        //    var args = new OltSwaggerArgs()
        //            .WithSecurityScheme(new OltSwaggerJwtBearerToken())
        //            .WithSecurityScheme(new OltSwaggerApiKey())
        //            .WithOperationFilter(new OltDefaultValueFilter())
        //            .Enable(true);

        //    //var options = new OltOptionsAspNetSwagger();

        //    var title = Assembly.GetEntryAssembly()?.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product ??
        //                Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product;

        //    var description = Assembly.GetEntryAssembly()?.GetCustomAttribute<System.Reflection.AssemblyDescriptionAttribute>()?.Description ??
        //                      Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyDescriptionAttribute>()?.Description;

        //    services.AddSwaggerVersioning(args);

        //    //Assert.False(options.);
        //    //Assert.Equal(title, options.Title);
        //    //Assert.Equal(description, options.Description);
        //    //Assert.NotNull(options.XmlSettings);
        //    //Assert.False(options.XmlSettings.IncludeControllerXmlComments);
        //    //Assert.Null(options.XmlSettings.CommentsFilePath);

        //    //title = Faker.Lorem.GetFirstWord();
        //    //description = Faker.Lorem.Sentence();

        //    //options = new OltOptionsAspNetSwagger
        //    //{
        //    //    Enabled = true,
        //    //    Title = title,
        //    //    Description = description,
        //    //    XmlSettings = new OltOptionsAspNetSwaggerXml
        //    //    {
        //    //        IncludeControllerXmlComments = true,
        //    //        CommentsFilePath = AppContext.BaseDirectory
        //    //    }
        //    //};

        //    //Assert.Equal(title, options.Title);
        //    //Assert.Equal(description, options.Description);
        //    //Assert.True(options.XmlSettings.IncludeControllerXmlComments);
        //    //Assert.Equal(AppContext.BaseDirectory, options.XmlSettings.CommentsFilePath);
        //}
    }
}