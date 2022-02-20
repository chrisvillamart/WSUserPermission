using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSUserPermission.Entities;

namespace WSUserPermission.Services
{
    public interface IPermissionService
    {
            PermissionResponse GetPermission();
            PermissionResponse RequestPermission(Permission permission);
            PermissionResponse ModifyPermission(Permission permission);
        
    }
}
