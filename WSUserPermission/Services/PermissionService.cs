using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using WSUserPermission.Context;
using WSUserPermission.Entities;

namespace WSUserPermission.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly DbContext _context;

        public PermissionService(DbContext ctx)
        {
            _context = ctx;
        }

        public PermissionResponse GetPermission()
        {
            Log.Information("GET Permission Service");
            var permissionResponse = new PermissionResponse();
            var listPermission = new List<Permission>();
            var result =
                  from p in _context.Permissions
                  join pt in _context.PermissionsTypes on p.Id equals pt.Id
                  select new
                  {
                      Id = p.Id,
                      EmployeeForename = p.EmployeeForename,
                      EmployeeSurname = p.EmployeeSurname,
                      PermissionType = new PermissionType(pt.Id, pt.Description),
                      PermissionDate = p.PermissionDate
                  };

            foreach (var item in result)
            {
                var permission = new Permission();

                Permission existingPermission = listPermission.Find(x => x.Id == item.Id);
                if (existingPermission == null)
                {
                    permission.Id = item.Id;
                    permission.EmployeeForename = item.EmployeeForename;
                    permission.EmployeeSurname = item.EmployeeSurname;
                    permission.listPermissionTypes = new List<PermissionType>();
                    permission.listPermissionTypes.Add(new PermissionType(item.PermissionType.Id, item.PermissionType.Description));
                    permission.PermissionDate = item.PermissionDate;
                    listPermission.Add(permission);
                }
                else
                {
                    existingPermission.listPermissionTypes.Add(new PermissionType(item.PermissionType.Id, item.PermissionType.Description));
                }
            }
            permissionResponse.data = listPermission;

            Log.Information("GET Permission Service");
            Log.Information($"GET Permission Response : {JsonConvert.SerializeObject(permissionResponse.data)}");
            return permissionResponse;
        }

        public PermissionResponse RequestPermission(Permission permission)
        {
            Log.Information("POST Permission Service");
            Log.Information("--PARAMETERS--");
            Log.Information($"EmployeeForename : {permission.EmployeeForename}"  );
            Log.Information($"EmployeeSurname : {permission.EmployeeSurname}");
            Log.Information($"PermissionType : {permission.PermissionType}" );
            Log.Information($"PermissionDate : {permission.PermissionDate}" );
            try
            {
                _context.Add(permission);
                _context.SaveChanges();
                Log.Information("Success POST Permission Service");
                return new PermissionResponse();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return new PermissionResponse(-1, "Ocurrió un error, revisar los logs");
            }

        }

        public PermissionResponse ModifyPermission(Permission permission)
        {
            Log.Information("PUT Permission Service");
            Log.Information("--PARAMETERS--");
            Log.Information($"Id : {permission.Id}");
            Log.Information($"EmployeeForename : {permission.EmployeeForename}");
            Log.Information($"EmployeeSurname : {permission.EmployeeSurname}");
            Log.Information($"PermissionType : {permission.PermissionType}");
            Log.Information($"PermissionDate : {permission.PermissionDate}");

            try
            {
                var modPermission = _context.Permissions.Where(d => d.Id == permission.Id).First();
                modPermission.EmployeeForename = permission.EmployeeForename;
                modPermission.EmployeeSurname = permission.EmployeeSurname;
                modPermission.PermissionType = permission.PermissionType;
                modPermission.PermissionDate = permission.PermissionDate;

                _context.SaveChanges();

                Log.Information("Success PUT Permission Service");
                return new PermissionResponse();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return new PermissionResponse(-1, "Ocurrió un error, revisar los logs");
            }
        }
    }
}
