using HealthyGamerPortal.Common.ViewModels.AuthorizationManagement;
using HealthyGamerPortal.Common.ViewModels.Users;
using HealthyGamerPortal.WEB.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Controllers.AuthorizationManagement
{
    public class AuthorizationManagementController : BaseController
    {
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> CreateProfile()
        {
            var model = new AuthorizationManagementViewModel() { RoleList = new List<AutherizationMangementProfileViewModel>(), AccessList = new List<AutherizationMangementProfileViewModel>() };

            var role1 = new AutherizationMangementProfileViewModel()
            {
                Name = "PowerShell - HG"
            };
            var role2 = new AutherizationMangementProfileViewModel()
            {
                Name = "PowerShell - HG"
            }; var role3 = new AutherizationMangementProfileViewModel()
            {
                Name = "PowerShell - HG"
            };
            var access1 = new AutherizationMangementProfileViewModel()
            {
                Name = "Create_User - HG"
            };
            var access2 = new AutherizationMangementProfileViewModel()
            {
                Name = "Create_User - HG"
            }; var access3 = new AutherizationMangementProfileViewModel()
            {
                Name = "Create_User - HG"
            };
            model.AccessList.Add(access1);
            model.AccessList.Add(access2);
            model.AccessList.Add(access3);

            model.RoleList.Add(role1);
            model.RoleList.Add(role2);
            model.RoleList.Add(role3);


            var data = new List<Common.ViewModels.Company.CreateCompanyBuildingViewModel>();
            var d1 = new Common.ViewModels.Company.CreateCompanyBuildingViewModel() { Name = "sdffsd", ContactEmail = "fsddsf@sfdfsd.nl" };
            var d2 = new Common.ViewModels.Company.CreateCompanyBuildingViewModel() { Name = "fsdd", ContactEmail = "sfdfds@sdfsdf.nl" };
            data.Add(d1); data.Add(d2);
            model.SelectList1 = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(data, "Name", "ContactEmail");

            return View(model);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> CreateProfile(CreateApplicationUserViewModel something)
        {
            return View("Index", "AuthorizationManagement");
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> CreateRole()
        {
            var model = new AuthorizationManagementViewModel() { AccessList = new List<AutherizationMangementProfileViewModel>() };

            var access1 = new AutherizationMangementProfileViewModel()
            {
                Name = "Create_User - HG"
            };
            var access2 = new AutherizationMangementProfileViewModel()
            {
                Name = "Create_User - HG"
            }; var access3 = new AutherizationMangementProfileViewModel()
            {
                Name = "Create_User - HG"
            };
            model.AccessList.Add(access1);
            model.AccessList.Add(access2);
            model.AccessList.Add(access3);
            return View(model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> CreateEntrance()
        {
            var model = new AuthorizationManagementViewModel() { ProfileList = new List<AutherizationMangementProfileViewModel>(), RoleList = new List<AutherizationMangementProfileViewModel>(), AccessList = new List<AutherizationMangementProfileViewModel>() };
            var profile1 = new AutherizationMangementProfileViewModel()
            {
                Name = "Management - HG"
            };
            var profile2 = new AutherizationMangementProfileViewModel()
            {
                Name = "Management - HG"
            }; var profile3 = new AutherizationMangementProfileViewModel()
            {
                Name = "Management - HG"
            };
            var role1 = new AutherizationMangementProfileViewModel()
            {
                Name = "PowerShell - HG"
            };
            var role2 = new AutherizationMangementProfileViewModel()
            {
                Name = "PowerShell - HG"
            }; var role3 = new AutherizationMangementProfileViewModel()
            {
                Name = "PowerShell - HG"
            };
            var access1 = new AutherizationMangementProfileViewModel()
            {
                Name = "Create_User - HG"
            };
            var access2 = new AutherizationMangementProfileViewModel()
            {
                Name = "Create_User - HG"
            }; var access3 = new AutherizationMangementProfileViewModel()
            {
                Name = "Create_User - HG"
            };
            model.AccessList.Add(access1);
            model.AccessList.Add(access2);
            model.AccessList.Add(access3);
            model.ProfileList.Add(profile1);
            model.ProfileList.Add(profile2);
            model.ProfileList.Add(profile3);
            model.RoleList.Add(role1);
            model.RoleList.Add(role2);
            model.RoleList.Add(role3);
            return View(model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Index()
        {
            var model = new AuthorizationManagementViewModel() { ProfileList = new List<AutherizationMangementProfileViewModel>(), RoleList = new List<AutherizationMangementProfileViewModel>(), AccessList = new List<AutherizationMangementProfileViewModel>() };
            var profile1 = new AutherizationMangementProfileViewModel()
            {
                Name = "Management - HG"
            };
            var profile2 = new AutherizationMangementProfileViewModel()
            {
                Name = "Management - HG"
            }; var profile3 = new AutherizationMangementProfileViewModel()
            {
                Name = "Management - HG"
            };
            var role1 = new AutherizationMangementProfileViewModel()
            {
                Name = "PowerShell - HG"
            };
            var role2 = new AutherizationMangementProfileViewModel()
            {
                Name = "PowerShell - HG"
            }; var role3 = new AutherizationMangementProfileViewModel()
            {
                Name = "PowerShell - HG"
            };
            var access1 = new AutherizationMangementProfileViewModel()
            {
                Name = "Create_User - HG"
            };
            var access2 = new AutherizationMangementProfileViewModel()
            {
                Name = "Create_User - HG"
            }; var access3 = new AutherizationMangementProfileViewModel()
            {
                Name = "Create_User - HG"
            };
            model.AccessList.Add(access1);
            model.AccessList.Add(access2);
            model.AccessList.Add(access3);
            model.ProfileList.Add(profile1);
            model.ProfileList.Add(profile2);
            model.ProfileList.Add(profile3);
            model.RoleList.Add(role1);
            model.RoleList.Add(role2);
            model.RoleList.Add(role3);
            return View(model);
        }
    }
}