using BizSol.Entities;
using BizSol.Models;
using BizSol.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BizSol.Services;

namespace BizSol.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        ApplicationDbContext db;
        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Home
        public ActionResult Index(string search, int? cityId, int? categoryId, int? pageNo)
        {
            HomeViewModel model = new HomeViewModel();
            model.totalAds = db.AdPosts.Include(i=>i.AdsImages).OrderByDescending(x => x.WhenAdded).ToList();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.CategoryId = categoryId;
            model.CityId = cityId;
            model.searchTerm = search;
            model.categories = db.Categories.ToList();
            int totalCount = HomeService.CategorySearchAdCount(search, cityId, categoryId);
            model.cities = HomeService.GetCity();
            model.adpost = HomeService.CategorySearchAd(search, cityId, categoryId, pageNo.Value, 9);

            model.Pager = new Pager(totalCount, pageNo);   
            return View(model);

        }
        public ActionResult _HomeSearchAds(string search, int? cityId, int? categoryId, int? pageNo)
        {
            SearchHomeViewModel model = new SearchHomeViewModel();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.CategoryId = categoryId;
            model.CityId = cityId;
            model.searchTerm = search;
            int totalCount = HomeService.CategorySearchAdCount(search, cityId, categoryId);

            model.adpost = HomeService.CategorySearchAd(search, cityId, categoryId, pageNo.Value, 9);

            model.Pager = new Pager(totalCount, pageNo);
            return PartialView(model);
        }
        //[Route("About")]
        public ActionResult AboutUs()
        {
            var adpost = db.AdPosts.Include(i=>i.AdsImages).Include(c=>c.Category).Take(4).OrderByDescending(x => x.WhenAdded).ToList();
            return View(adpost);
        }
        //[Route("Contact")]
        public ActionResult ContactUs()
        {
            ContactViewModel model = new ContactViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult ContactUs(ContactViewModel model)
        {
            if (model != null)
            {
                var contact = new Contact();
                contact.FullName = model.FullName;
                contact.Email = model.Email;
                contact.PhoneNumber = model.PhoneNumber;
                contact.Message_ = model.Message;
                db.Contact.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }

        }
    }
}