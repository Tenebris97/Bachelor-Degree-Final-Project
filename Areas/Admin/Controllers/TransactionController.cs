using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = (from t in _context.Transaction
                         join u in _context.Users on t.UserId equals u.Id
                         join o in _context.Order on t.OrderId equals o.OrderId
                         select new TransactionViewModel()
                         {
                             OrderId = o.OrderId,
                             TransactionNo = t.TransactionNo,
                             TransactionDate = t.TransactionDate,
                             TransactionTime = t.TransactionTime,
                             CustomerName = u.FirstName + " " + u.LastName,
                             Amount = t.Amount

                         }).AsNoTracking().OrderBy(p => p.OrderId);

            var modelPaging = await PagingList.CreateAsync(query, 10, page);
            return View(modelPaging);
        }
    }
}