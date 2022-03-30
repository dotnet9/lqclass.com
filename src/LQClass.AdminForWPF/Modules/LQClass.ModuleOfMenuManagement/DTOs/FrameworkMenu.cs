using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.ModuleOfMenuManagement.DTOs
{
    public class FrameworkMenu
    {
        public bool IsInside { get; set; }
        public string Url { get; set; }
        public string PageName { get; set; }
        public string ParentId { get; set; }
        public bool FolderOnly { get; set; }
        public bool ShowOnMenu { get; set; }
        public bool IsPublic { get; set; }
        public int DisplayOrder { get; set; }
        public string Icon { get; set; }
        public string SelectedModule { get; set; }
        public List<string> SelectedActionIDs { get; set; }
    }
}
