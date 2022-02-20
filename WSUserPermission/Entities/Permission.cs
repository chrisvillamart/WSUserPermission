using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WSUserPermission.Entities
{
    public class Permission
    {

        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PermissionType { get; set; } = null;
        public DateTime PermissionDate { get; set; }

        //FOR SELECT
        [NotMapped]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PermissionType> listPermissionTypes { get; set; }
    }
}
