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

namespace FinalProject.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Policy = "RequireMemberRole")]
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishlistController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = (from w in _context.Wishlist
                         join u in _context.Users on w.UserId equals u.Id
                         join p in _context.products on w.ProductId equals p.ProductId
                         where u.Id == _userManager.GetUserId(User)
                         select new WishlistViewModel()
                         {
                             WhishId = w.WhishId,
                             ProductName = p.ProductName,
                             ProductId = p.ProductId
                         }).AsNoTracking().OrderBy(p => p.ProductId);

            var modelPaging = await PagingList.CreateAsync(query, 10, page);
            return View(modelPaging);
        }

        public IActionResult Delete(int id)
        {
            using (var db = _context)
            {
                var model = db.Wishlist.Where(c => c.WhishId == id).SingleOrDefault();
                db.Wishlist.Remove(model);
                db.SaveChanges();
                return Ok();
            }
        }
    }
}