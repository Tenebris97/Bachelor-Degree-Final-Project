using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;

        public CategoryController(ApplicationDbContext context, IServiceProvider iServiceProvider)
        {
            _context = context;
            _iServiceProvider = iServiceProvider;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = _context.categories.AsNoTracking().OrderBy(c => c.CategoryId);
            var modelPaging = await PagingList.CreateAsync(model, 5, page);
            return View(modelPaging);
        }

        public async Task<IActionResult> Search(string categorySearch, int page = 1)
        {
            var model = _context.categories.AsNoTracking().OrderBy(c => c.CategoryId);

            //if null
            var modelPaging = await PagingList.CreateAsync(model, 5, page);

            if (categorySearch != null)
            {
                modelPaging = await PagingList.CreateAsync(
                    model.Where(c => c.Name.Contains(categorySearch)).OrderBy(c => c.CategoryId), 5, page);
            }

            return View("Index", modelPaging);
        }

        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            var model = new Category();

            if (id != 0)//حالت ویرایش
            {
                using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    //Agar peida kard
                    model = _context.categories.Where(c => c.CategoryId == id).SingleOrDefault();

                    //Agar peida nakone
                    if (model == null)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View("AddEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEdit(Category model, int id, string redirectUrl)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        db.categories.Add(model);
                        db.SaveChanges();
                    }
                    return PartialView("_SuccessfullResponse", redirectUrl);
                }

                else
                {
                    //update
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        db.categories.Update(model);
                        db.SaveChanges();
                    }
                    return PartialView("_SuccessfullResponse", redirectUrl);
                }
            }
            else
            {
                return View("AddEdit", model);
            }
        }

        public IActionResult Delete(int id)
        {
            using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                var model = db.categories.Where(c => c.CategoryId == id).SingleOrDefault();
                db.categories.Remove(model);
                db.SaveChanges();
                return Ok();
            }
        }
    }
}