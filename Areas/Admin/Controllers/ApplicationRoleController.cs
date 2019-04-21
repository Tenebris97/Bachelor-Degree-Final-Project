using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApplicationRoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public ApplicationRoleController(RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var allUserRoles = _context.UserRoles.ToList();
            //Database => ViewModel => Action => View
            List<RoleViewModel> model = new List<RoleViewModel>();//From Database
            //var allUserRoles = _identityDb.UserRoles.ToList();
            model = _roleManager.Roles.Select(r => new RoleViewModel//In ViewModel and used it in Action
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                NumberOfUsers = allUserRoles.Count(ur => ur.RoleId == r.Id)
            }).ToList();

            return View(model);
        }

        //For asp-action="AddRole" in _AddEditApplicationRole Partial View
        [HttpGet]
        public async Task<IActionResult> AddEdit(string Id)//Id Az asp-route-id miad inja
        {
            RoleViewModel model = new RoleViewModel();//Class RoleViewModel dar ViewModels

            if (!string.IsNullOrEmpty(Id))//حالت ویرایش
            {
                //پیدا کردن نقش از دیتابیس
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(Id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.Name = applicationRole.Name;
                    model.Description = applicationRole.Description;
                }
                //اگر پیدا نکرد
                else
                    return RedirectToAction("Index");
            }

            return View("AddEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(string Id, RoleViewModel model, string redirectUrl)
        {
            if (ModelState.IsValid)
            {
                bool exists = !String.IsNullOrEmpty(Id);
                ApplicationRole applicationRole = exists ? await _roleManager.FindByIdAsync(Id) : new ApplicationRole
                {
                    //Insert happens
                };
                applicationRole.Name = model.Name;
                applicationRole.Description = model.Description;

                IdentityResult result = exists ? await _roleManager.UpdateAsync(applicationRole)
                    : await _roleManager.CreateAsync(applicationRole);

                if (result.Succeeded)
                    return PartialView("_SuccessfullResponse", redirectUrl);
            }

            return View("AddEdit", model);
        }


        public async Task<IActionResult> Delete(string Id, IFormCollection from)//fomr faghat baraye ijad tamayoz estefade mishe
        {
            if (!String.IsNullOrEmpty(Id))
            {
                ApplicationRole ar = await _roleManager.FindByIdAsync(Id);
                if (ar != null)
                {
                    IdentityResult result = _roleManager.DeleteAsync(ar).Result;
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}