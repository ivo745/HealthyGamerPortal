
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HealthyGamerPortal.Common.ViewModels.AuthorizationManagement
{
    public class AuthorizationManagementViewModel
    {
        public string GenericName { get; set; }
        public string GenericDescription { get; set; }

        public List<AutherizationMangementProfileViewModel> ProfileList { get; set; }
        public List<AutherizationMangementProfileViewModel> RoleList { get; set; }
        public List<AutherizationMangementProfileViewModel> AccessList { get; set; }

        public SelectList SelectList1 { get; set; }

        public SelectList SelectList2 { get; set; }

        public string test { get; set; }

        public List<string> testList { get; set; }
    }
}
