using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Areas.User.Models;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Policy = "RequireMemberRole")]
    public class AddressController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AddressController(ApplicationDbContext context, IServiceProvider iServiceProvider,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _iServiceProvider = iServiceProvider;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var model = (from a in _context.Address
                         select new AddressViewModel
                         {
                             AddressId = a.AddressId,
                             FullAddress = a.FullAddress,
                             Postcode = a.Postcode,
                             UserId = userId
                         }).Where(a => a.UserId == userId).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            var model = new Address();

            if (id != 0)
            {
                //Update
                using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    model = _context.Address.Where(a => a.AddressId == id).SingleOrDefault();
                    if (model == null)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View("Add", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int AddressId, Address model, IEnumerable<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                model.UserId = userId;
                if (AddressId == 0)
                {
                    //Insert
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        db.Address.Add(model);
                        db.SaveChanges();
                    }
                    return Json(new { status = "success", message = "آدرس با موفقیت ثبت شد" });
                }
                else
                {
                    //Update
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        db.Address.Update(model);
                        db.SaveChanges();
                    }
                    return Json(new { status = "success", message = "آدرس با موفقیت ویراش شد" });
                }
            }

            //Display Validation with Jquery Ajax
            var list = new List<string>();
            foreach (var validation in ViewData.ModelState.Values)
                list.AddRange(validation.Errors.Select(error => error.ErrorMessage));

            return Json(new { status = "error", error = list });
        }
    }
}