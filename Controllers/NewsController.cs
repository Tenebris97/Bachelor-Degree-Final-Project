using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

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

        [HttpGet]
        public async Task<IActionResult> All(int page = 1)
        {
            var news = (from n in _context.news select n).AsNoTracking().OrderBy(n => n.NewsId);

            var modelPaging = await PagingList.CreateAsync(news, 10, page);
            return View(modelPaging);
        }
        public IActionResult NotFounds()
        {
            return View("NotFounds");
        }
    }
}