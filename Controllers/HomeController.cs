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
            int lapId = (from c in _context.categories where c.Name.Contains("لپ") select c.CategoryId).SingleOrDefault();
            int cellId = (from c in _context.categories where c.Name.Contains("گوشی") select c.CategoryId).SingleOrDefault();

            var model = new MultiModels();
            model.LastNews = (from n in _context.news orderby n.NewsId descending select n).Take(5).ToList();

            model.PromotionsListViewModel = (from pr in _context.promotion
                         join p in _context.products on pr.ProductId equals p.ProductId
                         join c in _context.categories on p.CategoryId equals c.CategoryId
                         select new PromotionListViewModel()
                         {
                             ProductName = p.ProductName,
                             ProductId = p.ProductId,
                             ProductImage = p.ProductImage,
                             PromotionId = pr.PromotionId,
                             Price = p.ProductPrice
                         }).Take(5).ToList();

           List<int> topSellingPId = (from o in _context.OrderDetails
                                 group o by o.ProductId into ts
                                 orderby ts.Count() descending
                                 select ts.Key).Take(5).ToList();

            List<TopSelling> topS = new List<TopSelling>();

            foreach (int id in topSellingPId)
            {
                var m = (from p in _context.products
                         where p.ProductId == id
                         select new TopSelling
                         {
                             ProductId = p.ProductId,
                             ProductImage = p.ProductImage,
                             ProductName = p.ProductName,
                             Price = p.ProductPrice
                         }).SingleOrDefault();

                topS.Add(m);
            }
            model.TopSellings = topS;

            model.MostViewd = (from p in _context.products
                               orderby p.ProductViews descending
                               select new MostViewd
                               {
                                   ProductName = p.ProductName,
                                   ProductId = p.ProductId,
                                   Price = p.ProductPrice,
                                   ProductImage = p.ProductImage
                               }).Take(5).ToList();

            model.Cheapest = (from p in _context.products
                              orderby p.ProductPrice ascending
                              select new Cheapest
                              {
                                  ProductName = p.ProductName,
                                  ProductId = p.ProductId,
                                  Price = p.ProductPrice,
                                  ProductImage = p.ProductImage
                              }).Take(5).ToList();

            model.TopLaptop = (from p in _context.products
                              orderby p.ProductId descending
                              where p.CategoryId == lapId
                              select new TopLaptop
                              {
                                  ProductName = p.ProductName,
                                  ProductId = p.ProductId,
                                  Price = p.ProductPrice,
                                  ProductImage = p.ProductImage
                              }).Take(5).ToList();

            model.TopCellphone = (from p in _context.products
                              orderby p.ProductId descending
                              where p.CategoryId == cellId
                              select new TopCellphone
                              {
                                  ProductName = p.ProductName,
                                  ProductId = p.ProductId,
                                  Price = p.ProductPrice,
                                  ProductImage = p.ProductImage
                              }).Take(5).ToList();                  

            ViewBag.imagepath = "/upload/normalimage/";
            return View(model);
        }

        public IActionResult Search(string txtSearch)
        {
            var model = new MultiModels();
            model.LastNews = (from n in _context.news orderby n.NewsId descending select n).Take(5).ToList();
            model.Products = (from p in _context.products 
                                where p.ProductName.Contains(txtSearch)
                                || p.ProductBrand.Contains(txtSearch)
                                orderby p.ProductId descending select p).ToList();
            ViewBag.imagepath = "/upload/normalimage/";
            ViewBag.Search = txtSearch;
            return View(model);                     
        }
        public IActionResult About()
        {
            var model = _context.AboutUs.FirstOrDefault();
            return View(model);
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
