using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class BigRoleViewModel
    {
        public IEnumerable<RoleViewModel> RoleViewModels { get; set; }
        public RoleViewModel RoleView { get; set; }
    }
}
