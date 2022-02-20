using Microsoft.Extensions.DependencyInjection;
using System;
using WSUserPermission.Context;
using WSUserPermission.Controllers;
using WSUserPermission.Entities;
using WSUserPermission.Services;
using Xunit;

namespace WSUserPermissionIntegrationTests
{
    public class PermissionTests
    {
        private readonly IPermissionService permissionService;

        public PermissionTests()
        {
            var services = new ServiceCollection();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddDbContext<DbContext>();

            var serviceProvider = services.BuildServiceProvider();

            permissionService = serviceProvider.GetService<IPermissionService>();
        }

        [Fact]
        public void GetPermissionType_HasAtLeastOne()
        {
            var result = permissionService.GetPermission();
            var listpermission = result.data;
            var hasPermissionType = true;

            foreach (var item in result.data) {
                hasPermissionType = item.listPermissionTypes.Count > 0;
            }

            Assert.True(hasPermissionType, "Algún elemento no tiene permission type");
        }
        [Fact]
        public void PostPermission_ShouldFailOnIdSent()
        {

            var permission = new Permission();
            permission.Id = 12;
            permission.EmployeeForename = "CARLA";
            permission.EmployeeSurname = "MALAFE";
            permission.PermissionType = 2;
            permission.PermissionDate = new DateTime();

            var result = permissionService.RequestPermission(permission);

            Assert.True(result.code == -1, "El servicio POST no está retornando error de Id en Insert");

        }

        [Fact]
        public void PutPermission_ShouldFailOnIdNotSent()
        {

            var permission = new Permission(); 
            permission.EmployeeForename = "CARLA";
            permission.EmployeeSurname = "MALAFE";
            permission.PermissionType = 2;
            permission.PermissionDate = new DateTime();

            var result = permissionService.ModifyPermission(permission);

            Assert.True(result.code == -1, "El servicio PUT no está retornando error cuando no se envía Id en Insert");

        }
    }
}
