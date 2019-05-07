using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("NotFounds");
            }

            var news = _context.news.SingleOrDefault(n => n.NewsId == id);

            if (news == null)
            {
                return RedirectToAction("NotFounds");
            }

            ViewBag.imagepath = "/upload/normalimage/";
            return View(news);
        }

        public IActionResult NotFounds()
        {
            return View("NotFounds");
        }
    }
}