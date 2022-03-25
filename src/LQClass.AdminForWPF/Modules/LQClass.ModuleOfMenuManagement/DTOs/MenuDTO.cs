using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.ModuleOfMenuManagement.DTOs
{
    public class MenuDTO
    {
        public string PageName { get; set; }
        public string ModuleName { get; set; }
        public string ShowOnMenu { get; set; }
        public string FolderOnly { get; set; }
        public string IsPublic { get; set; }
        public string DisplayOrder { get; set; }
        public string Icon { get; set; }
        public string ParentID { get; set; }
        public string TempIsSelected { get; set; }
        public string ID { get; set; }
        public bool LAY_CHECKED { get; set; }
    }
}
