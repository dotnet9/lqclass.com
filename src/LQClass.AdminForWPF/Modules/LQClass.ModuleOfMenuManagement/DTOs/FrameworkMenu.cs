using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.ModuleOfMenuManagement.DTOs
{
    public class FrameworkMenu
    {
        public string PageName { get; set; }
        public string ActionName { get; set; }
        public string ModuleName { get; set; }
        public bool FolderOnly { get; set; }
        public bool IsInherit { get; set; }
        public List<FunctionPrivilege> Privileges { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string Domain { get; set; }
        public bool ShowOnMenu { get; set; }
        public bool IsPublic { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsInside { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
    }
}
