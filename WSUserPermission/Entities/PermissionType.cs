using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSUserPermission.Entities
{
    public class PermissionType
    {
        public PermissionType(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; set; }
        public string Description { get; set; } 
    }
}
