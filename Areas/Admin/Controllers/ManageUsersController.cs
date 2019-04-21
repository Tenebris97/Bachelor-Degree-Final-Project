using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")] //Faghat Admin be in bakhsh dastreC dashte bashe
    public class ManageUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public ManageUsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            //نمایش اطلاعات به صورت صفحه‌بندی شده
            var query = _userManager.Users.AsNoTracking().Select(u => new UserListViewModel
            {
                Id = u.Id,
                FullName = u.FirstName + " " + u.LastName,
                Email = u.Email
            }).OrderBy(u => u.Id);
            var modelPaging = await PagingList.CreateAsync(query, 10, page);
            return View(modelPaging);
            /****************************************************************/
            //نمایش اطلاعات به صورت عادی
            //Database => ViewModel => Action => View
            //List<UserListViewModel> model = new List<UserListViewModel>();//From Database
            //model = _userManager.Users.Select(u => new UserListViewModel//In ViewModel and used it in Action
            //{
            //    Id = u.Id,
            //    FullName = u.FirstName + " " + u.LastName,
            //    Email = u.Email
            //}).ToList();

            //return View(model);//Send To View
        }

        public async Task<IActionResult> Search(string userSearch, int page = 1)
        {
            //نمایش اطلاعات به صورت صفحه‌بندی شده
            var query = _userManager.Users.AsNoTracking().Select(u => new UserListViewModel
            {
                Id = u.Id,
                FullName = u.FirstName + " " + u.LastName,
                Email = u.Email
            }).OrderBy(u => u.Id);
            var modelPaging = await PagingList.CreateAsync(query, 1, page);

            if (userSearch != null)
            {
                //model = model.Where(m => m.FullName.Contains(userSearch)).ToList();
                modelPaging = await PagingList.CreateAsync(
                    query.Where(u => u.FullName.Contains(userSearch)).OrderBy(u => u.Id), 1, page);
            }

            return View("Index", modelPaging);
        }

        [HttpGet]
        //Fills Role Dropdown list
        public IActionResult Add()
        {
            UserViewModel model = new UserViewModel();
            model.ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            return PartialView("Add", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserViewModel model, string redirectUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    //Database FirstName = FirstName from View
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Username,
                    Email = model.Email,
                    Gender = model.Gender
                };

                //Sabte etelaate bala dar DB be hamrahe password
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Id e naghshe morede nazar o peida kon
                    ApplicationRole role = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                    if (role != null)
                    {
                        //role ro baraye user ezafe kon
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, role.Name);
                        if (roleResult.Succeeded)
                        {
                            return PartialView("_SuccessfullResponse", redirectUrl);
                        }
                    }
                }
            }
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            EditUserViewModel model = new EditUserViewModel();
            model.ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            //Fetch User
            if (!String.IsNullOrWhiteSpace(Id))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(Id);
                if (user != null)
                {
                    model.FirstName = user.FirstName;
                    model.LastName = user.LastName;
                    model.Email = user.Email;
                    model.ApplicationRoleId = _roleManager.Roles.Single(r => r.Name == _userManager.GetRolesAsync(user).Result.SingleOrDefault()).Id;
                    model.Gender = user.Gender;
                }
            }

            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, EditUserViewModel model, string redirectUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(Id);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.Gender = model.Gender;
                    
                    string existingRole = _userManager.GetRolesAsync(user).Result.Single();
                    string existingRoleId = _roleManager.Roles.Single(r => r.Name == existingRole).Id;

                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (existingRole != model.ApplicationRoleId)
                        {
                            //If role had changed
                            //role-e jadid ba role-e ghabli replace mishe
                            IdentityResult roleResult = await _userManager.RemoveFromRoleAsync(user, existingRole);
                            if (roleResult.Succeeded)
                            {
                                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                                if (applicationRole != null)
                                {
                                    IdentityResult newRole = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                                    if (newRole.Succeeded)
                                        return PartialView("_SuccessfullResponse", redirectUrl);
                                }
                            }
                        }

                        //If role HAD NOT changed
                        return PartialView("_SuccessfullResponse", redirectUrl);
                    }
                }
            }
            //Refreshing Role Dropdown List
            model.ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(Id))
            {
                ApplicationUser au = await _userManager.FindByIdAsync(Id);
                if (au != null)
                    name = au.FirstName + " " + au.LastName;
            }
            return View("Delete", name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string Id, IFormCollection form)
        {
            ApplicationUser au = await _userManager.FindByIdAsync(Id);
            if (au != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(au);
                if (result.Succeeded)
                    return RedirectToAction("Index");
            }
            return View();
        }
    }
}