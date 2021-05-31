using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Web.Security;
using System.IO;

using System.Data.Entity;
using BizSol.Models;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using BizSol.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BizSol.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        ApplicationDbContext db;
        private ApplicationUserManager _userManager;

        public UserController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public UserController()
        {
            db = new ApplicationDbContext();
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
     


        public ActionResult DeleteAd(int? ID)
        {
            

            //AdPost p = db.AdPosts.Where(x => x.PostId == id).SingleOrDefault();
            AdPost ad = db.AdPosts.Where(x=>x.PostId == ID).FirstOrDefault();
            DeleteAdImages(ad.PostId);
            db.AdPosts.Remove(ad);            
            db.SaveChanges();
            
            return RedirectToAction("Usr_All_Ads", new { id = ad.UserID });
        }

        
        //.................................................................................................
        //Show user profile
        [HttpGet]
        [Authorize]
        public ActionResult Usr_profile()
        {
            string usrid = User.Identity.GetUserId();
            return View(UserManager.FindById(usrid));
        }
        //end user profile

        //Edit user profile
        [Authorize]
        public ActionResult Edit_usr()
        {

            var usr = UserManager.FindById(User.Identity.GetUserId());
            if (usr == null)
            {
                return RedirectToAction("login");
            }
            EditUserViewModel editUserViewModel = new EditUserViewModel();
            editUserViewModel.UserName = usr.UserName;
            editUserViewModel.Email = usr.Email;
            editUserViewModel.Contact = usr.PhoneNumber;
            return View(editUserViewModel);
        }
        //End of  Edit user profile

        //start changing user profile
        [HttpPost]
        public ActionResult Edit_usr(EditUserViewModel usr)
        {

            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                user.Email = usr.Email;
                user.PhoneNumber = usr.Contact;
                user.UserName = usr.UserName;
                var result = UserManager.Update(user);
            }
            return RedirectToAction("Usr_profile");
        }
        [Authorize]
        public ActionResult Usr_All_Ads(string id)
        {
            var Ads = db.AdPosts.Include(i=>i.AdsImages).Where(x => x.UserID == id).OrderByDescending(x=>x.WhenAdded).ToList();
            return View(Ads);
        }

        //................................................................................................
       
        public void DeleteAdImages(int adId)
        {
            var imgs= db.AdsImages.Where(i => i.AdId == adId).ToList();
            foreach(var img in imgs)
            {
                var filePath = Server.MapPath("~/Images/upload" + img.ImagePath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                db.AdsImages.Remove(img);
                db.SaveChanges();
            }
        }
    }
}
