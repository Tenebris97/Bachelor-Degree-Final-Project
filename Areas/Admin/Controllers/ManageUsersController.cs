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
    [Authorize(Policy = "RequireAdminRole")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public ManageUsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
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
      
    }
}