using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, IServiceProvider iServiceProvider, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _iServiceProvider = iServiceProvider;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //var model = new MultiModels();

            int productCount = (from p in _context.products select p).Count();
            int userCount = (from p in _context.Users select p).Count();
            int laptopCount = (from p in _context.laptops select p).Count();
            int cellphoneCount = (from p in _context.cellphones select p).Count();
            int promotionCount = (from p in _context.promotion select p).Count();

            ViewBag.ProductCount = productCount;
            ViewBag.UserCount = userCount;
            ViewBag.LaptopCount = laptopCount;
            ViewBag.CellphoneCount = cellphoneCount;
            ViewBag.PromotionCount = promotionCount;

            return View();
        }
    }
}