using System.Collections.Generic;
using WSUserPermission.Entities;

namespace WSUserPermission.Utils
{
    static class UtilClass
    {

        public static dynamic ProcessElasticSave(dynamic result)
        {
            var listPermission = new List<Permission>();
            var value = result.Value.data;
            foreach (var element in value)
            {
                foreach (var permissionType in element.listPermissionTypes)
                {
                    var permission = new Permission();
                    permission.Id = element.Id;
                    permission.EmployeeForename = element.EmployeeForename;
                    permission.EmployeeSurname = element.EmployeeSurname;
                    permission.PermissionType = permissionType.Id;
                    permission.PermissionDate = element.PermissionDate;
                    listPermission.Add(permission);
                }
            }
            return listPermission;
        }
    }
}
