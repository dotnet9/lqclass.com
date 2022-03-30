using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.ModuleOfMenuManagement.DTOs
{
    public class FunctionPrivilege
    {
        public string RoleCode { get; set; }

        public Guid MenuItemId { get; set; }

        public FrameworkMenu MenuItem { get; set; }

        public bool? Allowed { get; set; }
    }
}
