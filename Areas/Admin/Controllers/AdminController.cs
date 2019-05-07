using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
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
            //Tarikhe Shamsi
            var currentDate = DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(currentDate);
            int month = persianCalendar.GetMonth(currentDate);
            int day = persianCalendar.GetDayOfMonth(currentDate);
            string date = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(year + "/" + month + "/" + day));

            int productCount = (from p in _context.products select p).Count();
            int userCount = (from p in _context.Users select p).Count();
            int laptopCount = (from p in _context.laptops select p).Count();
            int cellphoneCount = (from p in _context.cellphones select p).Count();
            int promotionCount = (from p in _context.promotion select p).Count();
            int totalSell = (from p in _context.Order select p.Price).Sum();
            int totalOrder = (from p in _context.Order select p).Count();
            int todaySell = (from p in _context.Order where p.OrderDate == date select p.Price).Sum();
            int todayOrder = (from p in _context.Order where p.OrderDate == date select p).Count();
            int unprocessed = (from p in _context.Order where p.Flag == 1 || p.Flag == 2 select p).Count();

            ViewBag.ProductCount = productCount;
            ViewBag.UserCount = userCount;
            ViewBag.LaptopCount = laptopCount;
            ViewBag.CellphoneCount = cellphoneCount;
            ViewBag.PromotionCount = promotionCount;

            ViewBag.TotalSell = totalSell;
            ViewBag.TotalOrder = totalOrder;
            ViewBag.TodaySell = todaySell;
            ViewBag.TodayOrder = todayOrder;
            ViewBag.Unprocessed = unprocessed;

            return View();
        }
    }
}