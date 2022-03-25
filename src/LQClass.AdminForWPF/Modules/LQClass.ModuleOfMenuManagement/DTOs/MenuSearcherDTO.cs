using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.ModuleOfMenuManagement.DTOs
{
    public  class MenuSearcherDTO
    {
        public string PageName { get; set; }
      
        public string ModuleName { get; set; }
      
        public string ActionName { get; set; }
      
        public bool? ShowOnMenu { get; set; }
      
        public bool? IsPublic { get; set; }
      
        public bool? FolderOnly { get; set; }

        public Guid? RoleID { get; set; }


        public int Page { get; set; }

        public int Limit { get; set; }
    }
}
