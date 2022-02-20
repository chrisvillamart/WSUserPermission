using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WSUserPermission;
using WSUserPermissionIntegrationTests.WideWorldImporters.API.IntegrationTests;
using Xunit;

namespace WSUserPermissionIntegrationTests
{
    public class PermissionTest : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public PermissionTest(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task TestGet()
        {
            // Arrange
            var request = "api/permission/getPermission";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPost()
        {
            // Arrange
            var request = new
            {
                Url = "/api/permission/requestPermission",
                Body = new
                {

                    employeeForename = "CARLA",
                    employeeSurname = "MALAFE",
                    permissionType = 2,
                    permissionDate = new DateTime() 
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPut()
        {
            // Arrange
            var request = new
            {
                Url = "/api/permission/modifyPermission",
                Body = new
                {
                    Id = 1,
                    employeeForename = "CARLA",
                    employeeSurname = "MALAFE",
                    permissionType = 2,
                    permissionDate = new DateTime()
                }
            };

            // Act
            var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

    }
}
