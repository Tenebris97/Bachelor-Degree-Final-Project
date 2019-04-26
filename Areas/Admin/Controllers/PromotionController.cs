using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class PromotionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;

        public PromotionController(ApplicationDbContext context, IServiceProvider iServiceProvider)
        {
            _context = context;
            _iServiceProvider = iServiceProvider;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            //LINQ to Query string
            //Join on 3 tables
            var query = (from pr in _context.promotion
                         join p in _context.products on pr.ProductId equals p.ProductId
                         join c in _context.categories on p.CategoryId equals c.CategoryId
                         
                         select new PromotionListViewModel()
                         {
                             Category = c.Name,
                             ProductName = p.ProductName,
                             ProductId = p.ProductId,
                             PromotionId = pr.PromotionId
                         }).AsNoTracking().OrderBy(pr => pr.PromotionId);

            var modelPaging = await PagingList.CreateAsync(query, 5, page);
            return View(modelPaging);
        }

        [HttpGet]
        public IActionResult Add()
        {
            PromotionAddEdit model = new PromotionAddEdit();

            //Dropdown List - Products
            model.Products = _context.products.Select(c => new SelectListItem
            {
                Text = c.ProductName,
                Value = c.ProductId.ToString()
            }).ToList();

            return View("Add", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)//Id Az asp-route-id miad inja
        {
            PromotionAddEdit model = new PromotionAddEdit();
            //Dropdown List - Products
            model.Products = _context.products.Select(c => new SelectListItem
            {
                Text = c.ProductName,
                Value = c.ProductId.ToString()
            }).ToList();

            if (id != 0)
            {
                //Dependency Injection
                using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    Promotion promotion = _context.promotion.Where(p => p.PromotionId == id).SingleOrDefault();
                    if (promotion != null)
                    {
                        model.ProductId = promotion.ProductId;
                        model.PromotionId = promotion.PromotionId;
                    }
                }
            }
            return View("Add", model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int PromotionId, PromotionAddEdit model, IEnumerable<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                /***********************/
                if (PromotionId == 0)
                {
                    //Insert
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        Promotion promotionModel = AutoMapper.Mapper.Map<PromotionAddEdit, Promotion>(model);
                        db.promotion.Add(promotionModel);
                        db.SaveChanges();
                    }
                    return Json(new { status = "success", message = "پیشنهاد شگفت‌انگیز با موفقیت ثبت شد" });
                }
                else
                {
                    //Update
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        Promotion promotionModel = AutoMapper.Mapper.Map<PromotionAddEdit, Promotion>(model);
                        db.promotion.Update(promotionModel);
                        db.SaveChanges();
                    }
                    return Json(new { status = "success", message = "پیشنهاد شگفت‌انگیز  با موفقیت ویراش شد" });
                }
            }

            //Dropdown List - Products
            model.Products = _context.products.Select(c => new SelectListItem
            {
                Text = c.ProductName,
                Value = c.ProductId.ToString()
            }).ToList();
            //Display Validation with Jquery Ajax
            var list = new List<string>();
            foreach (var validation in ViewData.ModelState.Values)
                list.AddRange(validation.Errors.Select(error => error.ErrorMessage));

            return Json(new { status = "error", error = list });
        }

        public IActionResult Delete(int id)
        {
            using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                var model = db.promotion.Where(c => c.PromotionId == id).SingleOrDefault();
                db.promotion.Remove(model);
                db.SaveChanges();
                return Ok();
            }
        }

    }
}