using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;
        private readonly IHostingEnvironment _appEnvironment;

        public NewsController(IHostingEnvironment appEnvironment, ApplicationDbContext context, IServiceProvider iServiceProvider)
        {
            _context = context;
            _iServiceProvider = iServiceProvider;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = _context.news.AsNoTracking().Select(n => new News
            {
                NewsId = n.NewsId,
                NewsTitle = n.NewsTitle,
                NewsDate = n.NewsDate,
                NewsImage = n.NewsImage
            }).OrderBy(n => n.NewsId);

            var modelPaging = await PagingList.CreateAsync(model, 10, page);

            ViewBag.RootPath = "/upload/normalimage/";
            return View(modelPaging);
        }

        public async Task<IActionResult> Search(string fromDate1, string todate1, string newsSearch, int page = 1)
        {
            //List<News> model = new List<News>();

            var model = _context.news.Select(n => new News
            {
                NewsId = n.NewsId,
                NewsTitle = n.NewsTitle,
                NewsDate = n.NewsDate,
                NewsImage = n.NewsImage
            }).OrderBy(n => n.NewsId);

            var modelPaging = await PagingList.CreateAsync(model, 10, page);

            if (newsSearch != null)
            {
                modelPaging = await PagingList.CreateAsync(
                    model.Where(m => m.NewsTitle.Contains(newsSearch)).OrderBy(n => n.NewsId), 10, page);
            }
            //if (fromDate1 != null && todate1 == null)
            //{
            //    modelPaging = await PagingList.CreateAsync(
            //        model.Where(m => m.NewsDate.CompareTo(fromDate1) >= 0).OrderBy(r => r.NewsId), 10, page);
            //}
            //if (todate1 != null && fromDate1 == null)
            //{
            //    modelPaging = await PagingList.CreateAsync(
            //        model.Where(m => m.NewsDate.CompareTo(todate1) <= 0).OrderBy(r => r.NewsId), 10, page);
            //}
            //if (fromDate1 != null && todate1 != null)
            //{
            //    modelPaging = await PagingList.CreateAsync(
            //        model.Where(m => m.NewsDate.CompareTo(fromDate1) >= 0 &&
            //        m.NewsDate.CompareTo(todate1) <= 0).OrderBy(r => r.NewsId), 10, page);
            //}

            ViewBag.RootPath = "/upload/normalimage/";
            return View("Index", modelPaging);
        }

        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            var model = new News();

            if (id != 0)
            {
                //Update
                using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    model = _context.news.Where(n => n.NewsId == id).SingleOrDefault();
                    if (model == null)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            //بدست آوردن تاریخ شمسی
            var currentDate = DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(currentDate);
            int month = persianCalendar.GetMonth(currentDate);
            int day = persianCalendar.GetDayOfMonth(currentDate);
            string persianDate = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(year + "/" + month + "/" + day));
            ViewBag.pdate = persianDate;

            return View("AddEdit", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(int NewsId, News model, IEnumerable<IFormFile> files, string ImgName)
        {
            if (ModelState.IsValid)
            {
                //Upload Image
                var uploads = Path.Combine(_appEnvironment.WebRootPath, "upload\\normalimage\\");
                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        var filename = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        using (var fs = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                        {
                            await file.CopyToAsync(fs);
                            model.NewsImage = filename;
                        }
                    }
                }
                /***********************/
                if (NewsId == 0)
                {
                    if (model.NewsImage == null)
                    {
                        model.NewsImage = "defaultpic1.png";
                    }
                    //Insert
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        db.news.Add(model);
                        db.SaveChanges();
                    }
                    return Json(new { status = "success", message = "اطلاعیه با موفقیت ثبت شد" });
                }
                else
                {
                    if (model.NewsImage == null)
                    {
                        model.NewsImage = ImgName;
                    }
                    //Update
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        db.news.Update(model);
                        db.SaveChanges();
                    }
                    return Json(new { status = "success", message = "اطلاعیه با موفقیت ویراش شد" });
                }
            }

            //Display Validation with Jquery Ajax
            var list = new List<string>();
            foreach (var validation in ViewData.ModelState.Values)
                list.AddRange(validation.Errors.Select(error => error.ErrorMessage));

            return Json(new { status = "error", error = list });
        }

        public IActionResult Delete(int id)
        {
            using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                var model = _context.news.Where(b => b.NewsId == id).SingleOrDefault();

                //dastoorate hazf tasvir az server
                if (model.NewsImage != "defaultpic.png")
                {
                    var uploadsNormal = Path.Combine(_appEnvironment.WebRootPath, "upload\\normalimage\\") + model.NewsImage;
                    if (System.IO.File.Exists(uploadsNormal))
                        System.IO.File.Delete(uploadsNormal);
                }
                db.news.Remove(model);
                db.SaveChanges();
            }
            return Ok();
        }
    }
}