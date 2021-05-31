using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BizSol.Models;
using BizSol.Entities;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace BizSol.Controllers
{
    [HandleError]
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        ApplicationDbContext db;
        public AdminController()
        {
            db = new ApplicationDbContext();
        }
        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Admin


        [Authorize(Roles = "admin")]
        public ActionResult Index(int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var adpostlist = db.AdPosts.Include(img => img.AdsImages).OrderByDescending(x => x.WhenAdded).ToList();
            IPagedList<AdPost> adShow = adpostlist.ToPagedList(pageindex, pagesize);
            return View(adShow);
        }
        [HttpPost]
        public ActionResult Index(string searchAds, int? page)
        {
            var adslist = db.AdPosts.OrderByDescending(x => x.WhenAdded).ToList();
            if (searchAds != null)
            {
                adslist = db.AdPosts.Where(model => model.Title.ToLower().Contains(searchAds.ToLower())).OrderByDescending(x => x.WhenAdded).ToList();
            }
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<AdPost> adShow = adslist.ToPagedList(pageindex, pagesize);
            return View(adShow);
        }
        [Authorize(Roles = "admin")]
        public ActionResult DeleteAd(int? adId)
        {
            AdPost ad = db.AdPosts.Where(id=>id.PostId==adId).Include(i=>i.AdsImages).FirstOrDefault();
            DeleteAdImagesFromServer(ad.AdsImages.ToList());
            db.Entry(ad).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteUserAd(int? adId)
        {
            AdPost ad = db.AdPosts.Where(id => id.PostId == adId).Include(i => i.AdsImages).FirstOrDefault();
            DeleteAdImagesFromServer(ad.AdsImages.ToList());
            db.Entry(ad).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Ads","Admin",new { userId =ad.UserID});
        }
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var user = UserManager.FindByEmail(model.Email);
            var role = UserManager.GetRoles(user.Id);
            string roleName = "admin";
            if (role.Any(roleName.Contains))
            {
                var result = SignInManager.PasswordSignIn(user.UserName, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Index");
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = "", RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }

            return View();
        }
        [Authorize(Roles = "admin")]
        public ActionResult Ads(int? categoryId, int? cityId, string userId, int? page)
        {

            var adList = db.AdPosts.Include(img => img.AdsImages).OrderByDescending(x => x.WhenAdded).ToList();
            if (!string.IsNullOrEmpty(userId))
            {
                adList = db.AdPosts.Where(model => model.UserID == userId).OrderByDescending(model => model.WhenAdded).ToList();
            }
            if (categoryId.HasValue)
            {
                adList = db.AdPosts.Where(model => model.cat_Id == categoryId.Value).OrderByDescending(model => model.WhenAdded).ToList();
            }
            if (cityId.HasValue)
            {
                adList = db.AdPosts.Where(model => model.FkCityId == cityId.Value).OrderByDescending(model => model.WhenAdded).ToList();
            }
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<AdPost> adShow = adList.ToPagedList(pageindex, pagesize);
            return View(adShow);
        }
        [HttpPost]
        [Authorize(Roles ="admin")]
        public ActionResult Ads(int? categoryId, int? cityId, string userId, int? page, string searchAds)
        {
           

            var adList = db.AdPosts.Include(i=>i.AdsImages).OrderByDescending(x => x.WhenAdded).ToList();
            if (!string.IsNullOrEmpty(userId))
            {
                adList = db.AdPosts.Where(model => model.UserID == userId).OrderByDescending(model => model.WhenAdded).ToList();
            }
            if (categoryId.HasValue)
            {
                adList = db.AdPosts.Where(model => model.cat_Id == categoryId.Value).OrderByDescending(model => model.WhenAdded).ToList();
            }
            if (cityId.HasValue)
            {
                adList = db.AdPosts.Where(model => model.FkCityId == cityId.Value).OrderByDescending(model => model.WhenAdded).ToList();
            }
            if (!string.IsNullOrEmpty(searchAds))
            {
                adList = db.AdPosts.Where(ad => ad.Title.Contains(searchAds)).OrderByDescending(p => p.WhenAdded).ToList();
            }
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<AdPost> adShow = adList.ToPagedList(pageindex, pagesize);
            return View(adShow);
        }
        [Authorize(Roles = "admin")]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]

        [Authorize(Roles = "admin")]
        public ActionResult AddCategory(Category cvm)
        {

            Category cat = new Category();
            cat.cat_name = cvm.cat_name;
            //cat = path;
            db.Categories.Add(cat);
            db.SaveChanges();
            return RedirectToAction("ViewCategory");

        }

        // End Of Category Post Action


        //Start of View Action

        [Authorize(Roles = "admin")]
        public ActionResult ViewCategory()/*int? page*/
        {

            var categories = db.Categories.Include(ad => ad.AdPosts).ToList();



            return View(categories);


        }

        //End of view Category Action View

        //Catagory Edit 
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit_category(int categoryId)
        {

            var cat = db.Categories.Find(categoryId);
            return View(cat);
        }
        //Delete Category
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCategory(int? categoryId)
        {

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ads = db.AdPosts.Include(x => x.Category).Include(i => i.AdsImages).Where(x => x.cat_Id == categoryId).ToList();
                    foreach (var ad in ads)
                    {
                        DeleteAdImagesFromServer(ad.AdsImages.ToList());
                    }
                    db.AdPosts.RemoveRange(ads);
                    db.SaveChanges();

                    var category = db.Categories.Find(categoryId);
                    db.Categories.Remove(category);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return RedirectToAction("ViewCategory");
        }
        //del user method
        [Authorize(Roles = "admin")]
        public ActionResult DeleteUser(string userId)
        {
            //using (var transaction = db.Database.BeginTransaction())
            //{
                try
                {
                    var userads = db.AdPosts.Include(x => x.AdsImages).Where(x => x.UserID == userId).ToList();
                    foreach(var ad in userads)
                    {
                        DeleteAdImagesFromServer(ad.AdsImages.ToList());
                    }
                    db.AdPosts.RemoveRange(userads);  //first delete Ads of this User
                    db.SaveChanges();
                    var usr= UserManager.FindById(userId);
                    var logins = usr.Logins;
                    foreach(var login in logins)
                    {
                         UserManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                    }
                    var result=  UserManager.Delete(usr);
                    if (result.Succeeded)
                    {
                        //transaction.Commit();
                    }
                    else
                { 
                    //transaction.Rollback();
                }
                    
                }
                catch(Exception e)
                {
                   // transaction.Rollback();
                }
            //}
            
            return RedirectToAction("Usr_list");
        }

        //start of User Ads Detail
        [Authorize(Roles = "admin")]
        public ActionResult UserAds(string id, int? page)
        {
            var userAds = db.AdPosts.Where(model => model.UserID == id).OrderByDescending(model => model.PostId).ToList();
            return View(userAds);
        }

        //End of User Ads detail


        //End of Delete an ad

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit_cat(Category category)
        {
            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ViewCategory");
            //end of category edit...............
        }
        //start show all users list method
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Usr_list()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            var usr_list = db.Users.ToList();
            foreach(var user in usr_list)
            {
                if (!UserManager.IsInRole(user.Id, "admin"))
                {
                    users.Add(user);
                }
            }
            return View(users);
        }
        //End show all users list method


        //Start of Search Method
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Usr_list(string searchAds)
        {
            var userList = db.Users.ToList();
            if (searchAds != null)
            {
                userList = db.Users.Where(model => model.Email.Contains(searchAds)).ToList();
            }

            return View(userList);
        }

        //End of Search Method    
        
        [Authorize(Roles = "admin")]
        public ActionResult AddCity()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCity(City city)
        {

            City cities = new City();
            cities.CityName = city.CityName;
            db.Cities.Add(cities);
            db.SaveChanges();
            return RedirectToAction("ViewCity");
        }
        [Authorize(Roles = "admin")]
        public ActionResult UpdateCity(int? cityId)
        {

            var city = db.Cities.Find(cityId);

            return View(city);
        }
        [HttpPost]
        public ActionResult UpdateCity(City city)
        {

            City cities = new City();

            cities.CityName = city.CityName;
            db.Entry(cities).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ViewCity");
        }
        [Authorize(Roles = "admin")]
        public ActionResult ViewCity()/*int? page*/
        {
            var cities = db.Cities.Include(ad=>ad.AdPosts).ToList();
            return View(cities);
        }
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCity(int? cityId)
        {

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ads = db.AdPosts.Include(x => x.City).Include(i => i.AdsImages).Where(x => x.FkCityId==cityId).ToList();
                    foreach (var ad in ads)
                    {
                        DeleteAdImagesFromServer(ad.AdsImages.ToList());
                    }
                    db.AdPosts.RemoveRange(ads);
                    db.SaveChanges();

                    var city = db.Cities.Find(cityId);
                    db.Cities.Remove(city);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return RedirectToAction("ViewCity");
        }
        public void DeleteAdImagesFromServer(List<AdsImages> imgs)
        {
            try
            {
                //var img = db.AdsImages.Where(i => i.Id == id).FirstOrDefault();
                foreach (var img in imgs)
                {
                    var filePath = Server.MapPath("~" + img.ImagePath);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    db.AdsImages.Remove(img);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }

        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("login", "Admin");
        }
    }

}