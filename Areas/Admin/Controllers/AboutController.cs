using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinalProject.Models;
using FinalProject.Data;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;

        public AboutController(ApplicationDbContext context, IServiceProvider iServiceProvider)
        {
            _context = context;
            _iServiceProvider = iServiceProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            AboutUs model = _context.AboutUs.SingleOrDefault();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int AboutId, AboutUs model)
        {
            if (AboutId == 0)
            {
                using(var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                   db.AboutUs.Add(model);
                   db.SaveChanges();
                }
                return Json(new { status = "success", message = "درباره ما با موفقیت ثبت شد" });
            }
            else
            {
                using(var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                   db.AboutUs.Update(model);
                   db.SaveChanges();
                }
                return Json(new { status = "success", message = "درباره ما با موفقیت ویرایش شد" });
            }
        }
    }
}
