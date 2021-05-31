using BizSol.Entities;
using BizSol.Models;
using BizSol.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BizSol.Controllers
{
    [Authorize]
    [HandleError]
    public class AdPostController : Controller
    {
        ApplicationDbContext db;
        AdsImages imgs;
        List<City> li = null;
        public AdPostController()
        {
            db = new ApplicationDbContext();
            imgs = new AdsImages();
            li = db.Cities.ToList();
        }

        #region Category Selection
        //[Route("Category")]
        public ActionResult showCategory()
        {
            var list = db.Categories.ToList();
            return View(list);

        }
        public ActionResult CategoryDetail(int id)
        {
            var famousCategories = db.Categories.Where(s => s.cat_Status==1).Select(n=>n.cat_name).ToArray();
            TempData["TakeID"] = id;
            var categories = db.Categories.ToList();

            foreach (var category in categories)
            {
                if (id == category.cat_Id)
                {
                    if (famousCategories.Any(category.cat_name.Contains))
                    {
                        return RedirectToAction(category.cat_name);
                    }
                    else
                    {
                        return RedirectToAction("AdPost");
                    }
                }
            }

            return View();
        }
        #endregion

        public ActionResult EditAd(int adId)
        {

            var category = db.AdPosts.Include(c => c.Category).Where(id => id.PostId == adId).FirstOrDefault();
            string url = "/Adpost/" + category.Category.cat_name + "/" + adId;
            return RedirectToAction(category.Category.cat_name,new { adId=category.PostId});
        }

        #region Vehicle AdSubmit
        //[Route("Vehicle")]
        public ActionResult Vehicles(int? adId)
        {
            if (adId != 0&&adId!=null)
            {
                var adpost = db.AdPosts.Include(v => v.Vehicle).Include(img=>img.AdsImages).Include(c => c.Category).Include(ci => ci.City).Where(id => id.PostId == adId).FirstOrDefault();
                VehicleViewModel vehicleViewModel = new VehicleViewModel();
                vehicleViewModel.Vehicle_Id = adpost.Vehicle.Vehicle_Id;
                vehicleViewModel.AdsImages = adpost.AdsImages.ToList();
                vehicleViewModel.BrandName = adpost.Vehicle.BrandName;
                vehicleViewModel.cat_Id = adpost.Category.cat_Id;
                vehicleViewModel.CityID = adpost.City.CityID;
                vehicleViewModel.Condition = adpost.Condition;
                vehicleViewModel.Detail = adpost.Detail;
                vehicleViewModel.Vehicle_Milage = adpost.Vehicle.Vehicle_Milage;
                vehicleViewModel.Vehicle_Year = adpost.Vehicle.Vehicle_Year;
                vehicleViewModel.WhenAdded = adpost.WhenAdded;
                vehicleViewModel.IsFileAvailable = adpost.Vehicle.IsFileAvailable;
                vehicleViewModel.PostId = adpost.PostId;
                vehicleViewModel.Price = adpost.Price;
                vehicleViewModel.Title = adpost.Title;
                vehicleViewModel.UserID = User.Identity.GetUserId();
                vehicleViewModel.CityID = adpost.City.CityID;
                ViewBag.Citieslist = new SelectList(li, "CityID", "CityName",adpost.City.CityID);
                return View(vehicleViewModel);

            }
            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");
            return View(new VehicleViewModel());
        }


        [HttpPost]
        public ActionResult Vehicles(VehicleViewModel vehicleAdSubmit, List<HttpPostedFileBase> image_file)
        {

            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");

            int takeCatId = Convert.ToInt32(TempData["TakeID"]);
            if (vehicleAdSubmit.PostId != 0)
            {
                takeCatId = vehicleAdSubmit.cat_Id;
            }
            Vehicle vehicleAd = new Vehicle();
            vehicleAd.fk_cat_Id = takeCatId;
            vehicleAd.Vehicle_Milage = vehicleAdSubmit.Vehicle_Milage;
            vehicleAd.Vehicle_Year = vehicleAdSubmit.Vehicle_Year;
            vehicleAd.IsFileAvailable = vehicleAdSubmit.IsFileAvailable;
            vehicleAd.BrandName = vehicleAdSubmit.BrandName;
            if (vehicleAdSubmit.PostId != 0)
            {
                vehicleAd.Vehicle_Id = vehicleAdSubmit.Vehicle_Id;
                db.Entry(vehicleAd).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.Vehicles.Add(vehicleAd);
                db.SaveChanges();
            }
            


            int vehicleId = vehicleAd.Vehicle_Id;

            AdPost adPost = new AdPost();

            adPost.cat_Id = takeCatId;

            adPost.Title = vehicleAdSubmit.Title;
            adPost.Price = vehicleAdSubmit.Price;
            adPost.Condition = vehicleAdSubmit.Condition;
            adPost.Detail = vehicleAdSubmit.Detail;
            adPost.UserID = User.Identity.GetUserId();
            adPost.fk_vehicle_Id = vehicleId;
            adPost.WhenAdded = vehicleAdSubmit.WhenAdded;
            adPost.FkCityId = vehicleAdSubmit.CityID;
            if (vehicleAdSubmit.PostId != 0)
            {
                adPost.PostId = vehicleAdSubmit.PostId;
                db.Entry(adPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Usr_All_Ads", "User", new { id = adPost.UserID });
            }
            else
            {
                db.AdPosts.Add(adPost);
                db.SaveChanges();
            }
           
            SaveAdImages(image_file, adPost);

            return RedirectToAction("Index", "Home");
        }

       
        #endregion

        #region Bike AdSubmit
        //[Route("Bike")]
        public ActionResult Bikes(int? adId)
        {
            if (adId != 0 && adId != null)
            {
                var adpost = db.AdPosts.Include(v => v.Bike).Include(img => img.AdsImages).Include(c => c.Category).Include(ci => ci.City).Where(id => id.PostId == adId).FirstOrDefault();
                BikeViewModel bikeViewModel = new BikeViewModel();
                bikeViewModel.BikeID = adpost.Bike.BikeID;
                bikeViewModel.AdsImages = adpost.AdsImages.ToList();
                bikeViewModel.BrandName = adpost.Bike.BrandName;
                bikeViewModel.cat_Id = adpost.Category.cat_Id;
                bikeViewModel.CityID = adpost.City.CityID;
                bikeViewModel.Condition = adpost.Condition;
                bikeViewModel.Detail = adpost.Detail;
                bikeViewModel.BikeMileage = adpost.Bike.BikeMileage;
                bikeViewModel.BikeYear = adpost.Bike.BikeYear;
                bikeViewModel.WhenAdded = adpost.WhenAdded;
                bikeViewModel.IsFileAvailable = adpost.Bike.IsFileAvailable;
                bikeViewModel.PostId = adpost.PostId;
                bikeViewModel.Price = adpost.Price;
                bikeViewModel.Title = adpost.Title;
                bikeViewModel.UserID = User.Identity.GetUserId();
                bikeViewModel.CityID = adpost.City.CityID;
                ViewBag.Citieslist = new SelectList(li, "CityID", "CityName", adpost.City.CityID);
                return View(bikeViewModel);

            }


            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");
            return View(new BikeViewModel());
        }
        [HttpPost]
        public ActionResult Bikes(BikeViewModel bikeAdSubmit, List<HttpPostedFileBase> image_file)
        {
            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");

            int takeCatIdBike = Convert.ToInt32(TempData["TakeID"]);
            if (bikeAdSubmit.PostId != 0)
            {
                takeCatIdBike = bikeAdSubmit.cat_Id;
            }
            Bike bikeAd = new Bike();
            bikeAd.fk_Cat_Id = takeCatIdBike;
            bikeAd.BikeMileage = bikeAdSubmit.BikeMileage;
            bikeAd.BikeYear = bikeAdSubmit.BikeYear;
            bikeAd.BrandName = bikeAdSubmit.BrandName;
            bikeAd.IsFileAvailable = bikeAdSubmit.IsFileAvailable;
            if (bikeAdSubmit.PostId != 0)
            {
                bikeAd.BikeID = bikeAdSubmit.BikeID;
                db.Entry(bikeAd).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.Bikes.Add(bikeAd);
                db.SaveChanges();
            }
            

            int bikeId = bikeAd.BikeID;
            
            AdPost adPost = new AdPost();
            
            adPost.cat_Id = takeCatIdBike;
            adPost.Title = bikeAdSubmit.Title;
            adPost.Price = bikeAdSubmit.Price;
            adPost.Condition = bikeAdSubmit.Condition;
            adPost.Detail = bikeAdSubmit.Detail;
            adPost.UserID = User.Identity.GetUserId();
            adPost.fkBikeid = bikeId;
            adPost.FkCityId = bikeAdSubmit.CityID;
            adPost.WhenAdded = bikeAdSubmit.WhenAdded;
            if (bikeAdSubmit.PostId != 0)
            {
                adPost.PostId = bikeAdSubmit.PostId;
                db.Entry(adPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Usr_All_Ads", "User", new { id = adPost.UserID });
            }
            else
            {
                db.AdPosts.Add(adPost);
                db.SaveChanges();
            }
            
            SaveAdImages(image_file, adPost);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Mobile AdSubmit
        //[Route("Mobile")]
        public ActionResult Mobile(int? adId)
        {
            if (adId != 0 && adId != null)
            {
                var adpost = db.AdPosts.Include(v => v.Mobile).Include(img => img.AdsImages).Include(c => c.Category).Include(ci => ci.City).Where(id => id.PostId == adId).FirstOrDefault();
                MobileViewModel mobileViewModel = new MobileViewModel();
                mobileViewModel.MobilesId = adpost.Mobile.MobilesId;
                mobileViewModel.AdsImages = adpost.AdsImages.ToList();
                mobileViewModel.BrandName = adpost.Mobile.BrandName;
                mobileViewModel.cat_Id = adpost.Category.cat_Id;
                mobileViewModel.CityID = adpost.City.CityID;
                mobileViewModel.Condition = adpost.Condition;
                mobileViewModel.Detail = adpost.Detail;
                mobileViewModel.WhenAdded = adpost.WhenAdded;
                mobileViewModel.PostId = adpost.PostId;
                mobileViewModel.Price = adpost.Price;
                mobileViewModel.Title = adpost.Title;
                mobileViewModel.UserID = User.Identity.GetUserId();
                mobileViewModel.CityID = adpost.City.CityID;
                ViewBag.Citieslist = new SelectList(li, "CityID", "CityName", adpost.City.CityID);
                return View(mobileViewModel);

            }

            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");
           
            return View(new MobileViewModel());
        }
        [HttpPost]
        public ActionResult Mobile(MobileViewModel mobileAdSubmit, List<HttpPostedFileBase> image_file)
        {
            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");
           

            int takeCatId = Convert.ToInt32(TempData["TakeID"]);
            if (mobileAdSubmit.PostId != 0)
            {
                takeCatId = mobileAdSubmit.cat_Id;
            }
            Mobile mobile = new Mobile();
            
            mobile.BrandName = mobileAdSubmit.BrandName;
            mobile.fkCatId = takeCatId;
            if (mobileAdSubmit.PostId != 0)
            {
                mobile.MobilesId = mobileAdSubmit.MobilesId;
                db.Entry(mobile).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.Mobiles.Add(mobile);
                db.SaveChanges();
            }
            
            AdPost adPost = new AdPost();
            adPost.cat_Id = takeCatId;
            adPost.Title = mobileAdSubmit.Title;
            adPost.Price = mobileAdSubmit.Price;
           
            adPost.Condition = mobileAdSubmit.Condition;
            adPost.Detail = mobileAdSubmit.Detail;
            adPost.UserID = User.Identity.GetUserId();
            adPost.fkMobileId = mobile.MobilesId;
            adPost.WhenAdded = mobileAdSubmit.WhenAdded;
            adPost.FkCityId = mobileAdSubmit.CityID;
            if (mobileAdSubmit.PostId != 0)
            {
                adPost.PostId = mobileAdSubmit.PostId;
                db.Entry(adPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Usr_All_Ads", "User", new { id = adPost.UserID });
            }
            else
            {
                db.AdPosts.Add(adPost);
                db.SaveChanges();
            }
            
            SaveAdImages(image_file, adPost);
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Electronic AdSubmit
        //[Route("Electronic")]
        public ActionResult Electronic(int? adId)
        {
            if (adId != 0 && adId != null)
            {
                var adpost = db.AdPosts.Include(v => v.Electronic).Include(img => img.AdsImages).Include(c => c.Category).Include(ci => ci.City).Where(id => id.PostId == adId).FirstOrDefault();
                ElectronicViewModel electronicViewModel = new ElectronicViewModel();
                electronicViewModel.ElectronicId = adpost.Electronic.ElectronicId;
                electronicViewModel.AdsImages = adpost.AdsImages.ToList();
                electronicViewModel.Brandname = adpost.Electronic.BrandName;
                electronicViewModel.cat_Id = adpost.Category.cat_Id;
                electronicViewModel.CityID = adpost.City.CityID;
                electronicViewModel.Condition = adpost.Condition;
                electronicViewModel.Detail = adpost.Detail;
                electronicViewModel.ModelYear = adpost.Electronic.ModelYear;
                electronicViewModel.WhenAdded = adpost.WhenAdded;
                electronicViewModel.PostId = adpost.PostId;
                electronicViewModel.Price = adpost.Price;
                electronicViewModel.Title = adpost.Title;
                electronicViewModel.UserID = User.Identity.GetUserId();
                electronicViewModel.CityID = adpost.City.CityID;
                ViewBag.Citieslist = new SelectList(li, "CityID", "CityName", adpost.City.CityID);
                return View(electronicViewModel);

            }



            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");
            return View(new ElectronicViewModel());
        }
        [HttpPost]
        public ActionResult Electronic(ElectronicViewModel electronicAdSubmit, List<HttpPostedFileBase> image_file)
        {
            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");

            int takeCatId = Convert.ToInt32(TempData["TakeID"]);
            if (electronicAdSubmit.PostId != 0)
            {
                takeCatId = electronicAdSubmit.cat_Id;
            }
            Electronic electronic = new Electronic();
            electronic.FkCatID = takeCatId;
            electronic.BrandName = electronicAdSubmit.Brandname;
            electronic.ModelYear = electronicAdSubmit.ModelYear;
            if (electronicAdSubmit.PostId != 0)
            {
                electronic.ElectronicId = electronicAdSubmit.ElectronicId;
                db.Entry(electronic).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.Electronics.Add(electronic);
                db.SaveChanges();
            }
           
            AdPost adPost = new AdPost();
            adPost.cat_Id = takeCatId;
            adPost.Title = electronicAdSubmit.Title;
            adPost.Price = electronicAdSubmit.Price;
           
            adPost.Condition = electronicAdSubmit.Condition;
            adPost.Detail = electronicAdSubmit.Detail;
            adPost.UserID = User.Identity.GetUserId();
            adPost.fkElectronicId = electronic.ElectronicId;
            adPost.FkCityId = electronicAdSubmit.CityID;
            adPost.WhenAdded = electronicAdSubmit.WhenAdded;
            if (electronicAdSubmit.PostId != 0)
            {
                adPost.PostId = electronicAdSubmit.PostId;
                db.Entry(adPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Usr_All_Ads", "User", new { id = adPost.UserID });
            }
            else
            {
                db.AdPosts.Add(adPost);
                db.SaveChanges();
            }
           
            SaveAdImages(image_file, adPost);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Property AdSubmit
        //[Route("Property")]
        public ActionResult Property(int? adId)
        {
            if (adId != 0 && adId != null)
            {
                var adpost = db.AdPosts.Include(v => v.Property).Include(img => img.AdsImages).Include(c => c.Category).Include(ci => ci.City).Where(id => id.PostId == adId).FirstOrDefault();
                PropertyViewModel propertyViewModel = new PropertyViewModel();
                propertyViewModel.PropertyId = adpost.Property.PropertyId;
                propertyViewModel.AdsImages = adpost.AdsImages.ToList();
                propertyViewModel.AreaType = adpost.Property.AreaType;
                propertyViewModel.cat_Id = adpost.Category.cat_Id;
                propertyViewModel.CityID = adpost.City.CityID;
                propertyViewModel.Condition = adpost.Condition;
                propertyViewModel.Detail = adpost.Detail;
                propertyViewModel.PropertyArea = adpost.Property.PropertyArea;
                propertyViewModel.WashRoom = adpost.Property.WashRoom;
                propertyViewModel.WhenAdded = adpost.WhenAdded;
                propertyViewModel.BedRoom = adpost.Property.BedRoom;
                propertyViewModel.PostId = adpost.PostId;
                propertyViewModel.Price = adpost.Price;
                propertyViewModel.Title = adpost.Title;
                propertyViewModel.UserID = User.Identity.GetUserId();
                propertyViewModel.CityID = adpost.City.CityID;
                propertyViewModel.IsCarParking = adpost.Property.IsCarParking;
                ViewBag.Citieslist = new SelectList(li, "CityID", "CityName", adpost.City.CityID);
                return View(propertyViewModel);
            }

            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");
            return View(new PropertyViewModel());
        }
        [HttpPost]
        public ActionResult Property(PropertyViewModel PropertyAdSubmit, List<HttpPostedFileBase> image_file)
        {
           
            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");

            int takeCatId = Convert.ToInt32(TempData["TakeID"]);
            if (PropertyAdSubmit.PostId != 0)
            {
                takeCatId = PropertyAdSubmit.cat_Id;
            }
            Property propertyAd = new Property();
            propertyAd.FKCatId = takeCatId;
            propertyAd.AreaType = PropertyAdSubmit.AreaType;
            propertyAd.PropertyArea = PropertyAdSubmit.PropertyArea;
            propertyAd.WashRoom = PropertyAdSubmit.WashRoom;
            propertyAd.BedRoom = PropertyAdSubmit.BedRoom;
            propertyAd.IsCarParking = PropertyAdSubmit.IsCarParking;
            if (PropertyAdSubmit.PostId != 0)
            {
                propertyAd.PropertyId = PropertyAdSubmit.PropertyId;
                db.Entry(propertyAd).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.Properties.Add(propertyAd);
                db.SaveChanges();
            }
            
            int propertyId = propertyAd.PropertyId;


            AdPost adPost = new AdPost();
            
            adPost.cat_Id = takeCatId;
            adPost.Title = PropertyAdSubmit.Title;
            adPost.Price = PropertyAdSubmit.Price;
            adPost.Condition = PropertyAdSubmit.Condition;
            adPost.Detail = PropertyAdSubmit.Detail;
            adPost.UserID = User.Identity.GetUserId();
            adPost.fkPropertyId = propertyId;
            adPost.FkCityId = PropertyAdSubmit.CityID;
            adPost.WhenAdded = PropertyAdSubmit.WhenAdded;
            if (PropertyAdSubmit.PostId != 0)
            {
                adPost.PostId = PropertyAdSubmit.PostId;
                db.Entry(adPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Usr_All_Ads", "User", new { id = adPost.UserID });
            }
            else
            {
                db.AdPosts.Add(adPost);
                db.SaveChanges();
            }
            
            SaveAdImages(image_file, adPost);
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Dress AdSubmit
        //[Route("Cloth")]
        public ActionResult Dress(int? adId)
        {

            if (adId != 0 && adId != null)
            {
                var adpost = db.AdPosts.Include(v => v.Cloth).Include(img => img.AdsImages).Include(c => c.Category).Include(ci => ci.City).Where(id => id.PostId == adId).FirstOrDefault();
                ClothViewModel clothViewModel = new ClothViewModel();
                clothViewModel.ClothId = adpost.Cloth.ClothId;
                clothViewModel.AdsImages = adpost.AdsImages.ToList();
                clothViewModel.ClothType = adpost.Cloth.ClothType;
                clothViewModel.cat_Id = adpost.Category.cat_Id;
                clothViewModel.CityID = adpost.City.CityID;
                clothViewModel.Condition = adpost.Condition;
                clothViewModel.Detail = adpost.Detail;
                clothViewModel.Color = adpost.Cloth.Color;
                clothViewModel.WhenAdded = adpost.WhenAdded;
                clothViewModel.Size = adpost.Cloth.Size;
                clothViewModel.PostId = adpost.PostId;
                clothViewModel.Price = adpost.Price;
                clothViewModel.Title = adpost.Title;
                clothViewModel.UserID = User.Identity.GetUserId();
                clothViewModel.CityID = adpost.City.CityID;
                ViewBag.Citieslist = new SelectList(li, "CityID", "CityName", adpost.City.CityID);
                return View(clothViewModel);
            }

            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");
            return View(new ClothViewModel());
        }
        [HttpPost]
        public ActionResult Dress(ClothViewModel clothAdSubmit, List<HttpPostedFileBase> image_file)
        {
           
            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");

            int takeCatId = Convert.ToInt32(TempData["TakeID"]);
            if (clothAdSubmit.PostId != 0)
            {
                takeCatId = clothAdSubmit.cat_Id;
            }

            Cloth cloth = new Cloth();
            cloth.FkCatId = takeCatId;
            cloth.ClothType = clothAdSubmit.ClothType;
            cloth.Size = clothAdSubmit.Size;
            cloth.Color = clothAdSubmit.Color;
            if (clothAdSubmit.PostId != 0)
            {
                cloth.ClothId = clothAdSubmit.ClothId;
                db.Entry(cloth).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.Clothes.Add(cloth);
                db.SaveChanges();
            }
            

            int clothId = cloth.ClothId;


            AdPost adPost = new AdPost();
            adPost.cat_Id = takeCatId;
            adPost.Title = clothAdSubmit.Title;
            adPost.Price = clothAdSubmit.Price;
            
            adPost.Condition = clothAdSubmit.Condition;
            adPost.Detail = clothAdSubmit.Detail;
            adPost.UserID = User.Identity.GetUserId();
            adPost.fkClothid = clothId;
            adPost.FkCityId = clothAdSubmit.CityID;
            adPost.WhenAdded = clothAdSubmit.WhenAdded;
            if (clothAdSubmit.PostId != 0)
            {
                adPost.PostId = clothAdSubmit.PostId;
                db.Entry(adPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Usr_All_Ads","User", new { id = adPost.UserID });
            }
            else
            {
                db.AdPosts.Add(adPost);
                db.SaveChanges();
            }
            
            SaveAdImages(image_file, adPost);
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Other categories
        [HttpGet]
        public ActionResult AdPost(int? adId)
        {
            if (adId != 0 && adId != null)
            {
                var adpost = db.AdPosts.Include(img => img.AdsImages).Include(c => c.Category).Include(ci => ci.City).Where(id => id.PostId == adId).FirstOrDefault();
                AdPostViewModel adPostViewModel  = new AdPostViewModel();
                adPostViewModel.AdsImages = adpost.AdsImages.ToList();
                adPostViewModel.cat_Id = adpost.Category.cat_Id;
                adPostViewModel.CityID = adpost.City.CityID;
                adPostViewModel.Condition = adpost.Condition;
                adPostViewModel.Detail = adpost.Detail;
                adPostViewModel.WhenAdded = adpost.WhenAdded;
                adPostViewModel.PostId = adpost.PostId;
                adPostViewModel.Price = adpost.Price;
                adPostViewModel.Title = adpost.Title;
                adPostViewModel.UserID = User.Identity.GetUserId();
                adPostViewModel.CityID = adpost.City.CityID;
                ViewBag.Citieslist = new SelectList(li, "CityID", "CityName", adpost.City.CityID);
                return View(adPostViewModel);
            }

            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");
            return View(new AdPostViewModel());
        }
        [HttpPost]
        public ActionResult AdPost(AdPostViewModel adPostViewModel,List<HttpPostedFileBase> image_file)
        {
            ViewBag.Citieslist = new SelectList(li, "CityID", "CityName");

            int takeCatId = Convert.ToInt32(TempData["TakeID"]);
            if (adPostViewModel.PostId != 0)
            {
                takeCatId = adPostViewModel.cat_Id;
            }

            AdPost adPost = new AdPost();
            adPost.cat_Id = takeCatId;
            adPost.Title = adPostViewModel.Title;
            adPost.Price = adPostViewModel.Price;

            adPost.Condition = adPostViewModel.Condition;
            adPost.Detail = adPostViewModel.Detail;
            adPost.UserID = User.Identity.GetUserId();
            adPost.FkCityId = adPostViewModel.CityID;
            adPost.WhenAdded = adPostViewModel.WhenAdded;
            if (adPostViewModel.PostId != 0)
            {
                adPost.PostId = adPostViewModel.PostId;
                db.Entry(adPost).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.AdPosts.Add(adPost);
                db.SaveChanges();
            }

            SaveAdImages(image_file, adPost);
            return RedirectToAction("Index", "Home");
        }
        #endregion
        #region Image Upload Action
        private void SaveAdImages(List<HttpPostedFileBase> image_file, AdPost adPost)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file.ContentLength != 0)
                {
                    var path = uploadimgfile(image_file[i]);
                    path = path.Split('~')[1];
                    imgs.AdId = adPost.PostId;
                    imgs.ImagePath = path;
                    db.AdsImages.Add(imgs);
                    db.SaveChanges();
                }
            }
        }

        public string uploadimgfile(HttpPostedFileBase file)
        {

            Random r = new Random();
            string path = "-1";
            int random = r.Next();

            if (file != null)/* && file.ContentLength > 0*/
            {

                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {

                        path = Path.Combine(Server.MapPath("~/Images/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Images/upload/" + random + Path.GetFileName(file.FileName);

                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please select a file'); </script>");
                    path = "-1";

                }



            }



            return path;


        }
        [HttpPost]
        public JsonResult DeleteAdImage(int? id)
        {
            try
            {
                var img = db.AdsImages.Where(i => i.Id == id).FirstOrDefault();
                if (img != null)
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
            catch(Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            return Json("success", JsonRequestBehavior.AllowGet);

        }
        #endregion




    }
}