using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using FinalProject.Data;
using FinalProject.Models.ViewModels;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;

        public HomeController(ApplicationDbContext context, IServiceProvider iServiceProvider)
        {
            _context = context;
            _iServiceProvider = iServiceProvider;
        }

        public IActionResult Index()
        {
            var model = new MultiModels();
            model.LastNews = (from n in _context.news orderby n.NewsId descending select n).Take(5).ToList();
            model.PromotionsListViewModel = (from pr in _context.promotion
                         join p in _context.products on pr.ProductId equals p.ProductId
                         join c in _context.categories on p.CategoryId equals c.CategoryId

                         select new PromotionListViewModel()
                         {
                             Category = c.Name,
                             ProductName = p.ProductName,
                             ProductId = p.ProductId,
                             ProductImage = p.ProductImage,
                             PromotionId = pr.PromotionId
                         }).Take(5).ToList();

            ViewBag.imagepath = "/upload/normalimage/";
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
