using BizSol.Entities;
using BizSol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BizSol.Models.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using BizSol.Services;

namespace BizSol.Controllers
{
    [HandleError]
    public class AdsController : Controller
    {
        ApplicationDbContext db;
        private ApplicationUserManager _userManager;
        public AdsController(ApplicationUserManager userManager)
        {
            
            UserManager = userManager;
        }
        public AdsController()
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
        public ActionResult LayoutFooterAds()
        {
            AdsViewModel model = new AdsViewModel();

            model.adpost = db.AdPosts.OrderByDescending(x => x.WhenAdded).ToList();
            return PartialView(model);
        }
        // GET: Ads
        //[Route("Category/{search?:categoryId?:cityId?:pageNo?:minimumPrice?:maximumPrice?:selectedCategoryId?:selectedCityId?:}")]
        //[Route("Category/{categoryId?}")]

        public ActionResult Index(string search, int? categoryId, int? cityId, int? pageNo, int? minimumPrice, int? maximumPrice, int? selectedCategoryId, int? selectedCityId, int? sortBy, string condition)/* int? page string search*/
        {
            AdsViewModel model = new AdsViewModel();
            //TempData["takeCategoryId"] = categoryId;
            if (categoryId.HasValue)
            {
                pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
                model.categoryId = (int)(categoryId);               
                model.SortBy = sortBy;
                model.Condition = condition;
                model.SelectedCityId = selectedCityId;
                model.searchTerm = search;
                model.cities = AdServices.GetCity();
                //model.MaximumPrice = AdServices.GetMaximumPrice(categoryId);
                int totalCount = AdServices.CategorySearchAdCount(search, minimumPrice, maximumPrice, selectedCategoryId, selectedCityId, categoryId, sortBy, condition);

                model.adpost = AdServices.CategorySearchAd(search, minimumPrice, maximumPrice, selectedCategoryId, selectedCityId, categoryId, sortBy, condition, pageNo.Value,9);
               
                model.Pager = new Pager(totalCount, pageNo);
                return View(model);
            }
            if (cityId.HasValue)
            {
                model.cityId = (int)(cityId);
                model.categories = AdServices.GetCategory();
                //model.MaximumPrice = AdServices.GetMaximumPrice(categoryId);
                model.adpost = AdServices.CitySearchAd(search, minimumPrice, maximumPrice, selectedCategoryId, selectedCityId, cityId);

                int totalCount = AdServices.CategorySearchAdCount(search, minimumPrice, maximumPrice, selectedCategoryId, selectedCityId, categoryId, sortBy, condition);

                model.Pager = new Pager(totalCount, pageNo);
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }

        }

        public ActionResult FilterAds(string search, int? categoryId, int? cityId, int? pageNo, int? minimumPrice, int? maximumPrice, int? selectedCategoryId, int? selectedCityId,int? sortBy,string condition)/* int? page string search*/
        {
            FilterAdsViewModel model = new FilterAdsViewModel();
            //categoryId = Convert.ToInt32(TempData["takeCategoryId"]);
            if (categoryId.HasValue)
            {
                pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
                model.categoryId = (int)(categoryId);
                model.searchTerm = search;
                model.Condition = condition;
                model.SortBy = sortBy;
                model.SelectedCityId = selectedCityId;
                model.searchTerm = search;
                int totalCount = AdServices.CategorySearchAdCount(search, minimumPrice, maximumPrice, selectedCategoryId, selectedCityId, categoryId, sortBy, condition);

                model.adpost = AdServices.CategorySearchAd(search, minimumPrice, maximumPrice, selectedCategoryId, selectedCityId, categoryId, sortBy,condition, pageNo.Value, 9);
                
                model.Pager = new Pager(totalCount, pageNo);
                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }
            
            
        }
        //[Route("City/{cityId?}")]
        public ActionResult CityAds(string search, int? categoryId, int? cityId, int? pageNo, int? minimumPrice, int? maximumPrice, int? selectedCategoryId, int? selectedCityId, int? sortBy, string condition)
        {
            CityViewModel model = new CityViewModel();
            //TempData["takeCategoryId"] = categoryId;
            if (categoryId.HasValue)
            {
                pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
                model.categoryId = (int)(categoryId);
                model.SortBy = sortBy;
                model.Condition = condition;
                model.SelectedCityId = selectedCityId;
                model.searchTerm = search;
                model.cities = AdServices.GetCity();
                //model.MaximumPrice = AdServices.GetMaximumPrice(categoryId);
                int totalCount = AdServices.CategorySearchAdCount(search, minimumPrice, maximumPrice, selectedCategoryId, selectedCityId, categoryId, sortBy, condition);

                model.adpost = AdServices.CategorySearchAd(search, minimumPrice, maximumPrice, selectedCategoryId, selectedCityId, categoryId, sortBy, condition, pageNo.Value, 9);

                model.Pager = new Pager(totalCount, pageNo);
                return View(model);
            }
            if (cityId.HasValue)
            {
                pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
                model.cityId = (int)(cityId);
                model.SortBy = sortBy;
                model.Condition = condition;
                model.SelectedCategoryId = selectedCategoryId;
                model.SelectedCityId = selectedCityId;
                model.searchTerm = search;
                //model.cities = AdServices.GetCity();
                //model.MaximumPrice = AdServices.GetMaximumPrice(categoryId);
                model.categories = CityServices.GetCategory();
                int totalCount = CityServices.CitySearchAdCount(search, minimumPrice, maximumPrice, selectedCategoryId, selectedCityId, cityId, sortBy, condition);

                model.adpost = CityServices.CitySearchAd(search, minimumPrice, maximumPrice, selectedCategoryId, selectedCityId, cityId, sortBy, condition, pageNo.Value, 9);

                model.Pager = new Pager(totalCount, pageNo);
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult _cityFilterAds(string search, int? categoryId, int? cityId, int? pageNo, int? minimumPrice, int? maximumPrice, int? selectedCategoryId, int? selectedCityId, int? sortBy, string condition)
        {
            CityFilterViewModel model = new CityFilterViewModel();
            if (cityId.HasValue)
            {
                pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
                model.cityId = (int)(cityId);
                model.SortBy = sortBy;
                model.Condition = condition;
                model.SelectedCategoryId = selectedCategoryId;
                model.SelectedCityId = selectedCityId;
                model.searchTerm = search;
                //model.cities = AdServices.GetCity();
                //model.MaximumPrice = AdServices.GetMaximumPrice(categoryId);
                //model.categories = CityServices.GetCategory();
                int totalCount = CityServices.CitySearchAdCount(search, minimumPrice, maximumPrice, selectedCategoryId, selectedCityId, cityId, sortBy, condition);

                model.adpost = CityServices.CitySearchAd(search, minimumPrice, maximumPrice, selectedCategoryId, selectedCityId, cityId, sortBy, condition, pageNo.Value, 9);

                model.Pager = new Pager(totalCount, pageNo);
                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        //[Route("ViewAD/{id?}")]
        public ActionResult ViewAd(int? id)
        {

           
            AdViewModel adViewModel = new AdViewModel();
            AdPost adpost = db.AdPosts.Include(img=>img.AdsImages).Include(c=>c.Category).Include(u=>u.User).Where(x => x.PostId == id).SingleOrDefault();
            if (adpost != null)
            {
                var city = db.Cities.Find(adpost.FkCityId);

                adViewModel.CityID = city.CityID;
                adViewModel.CityName = city.CityName;
                adViewModel.PostId = adpost.PostId;
                adViewModel.Cat_Id = adpost.Category.cat_Id;
                adViewModel.Condition = adpost.Condition;
                adViewModel.Title = adpost.Title;
                adViewModel.Detail = adpost.Detail;
                adViewModel.AdsImages = adpost.AdsImages.ToList();
                adViewModel.Price = adpost.Price;
                adViewModel.WhenAdded = adpost.WhenAdded;
                adViewModel.UserName = adpost.User.UserName;
                if (adpost.fk_vehicle_Id != null)
                {
                    Vehicle vehicle = db.Vehicles.Where(x => x.Vehicle_Id == adpost.fk_vehicle_Id).SingleOrDefault();
                    VehicleViewModel vehicleViewModel = new VehicleViewModel();
                    vehicleViewModel.Vehicle_Id = vehicle.Vehicle_Id;
                    vehicleViewModel.Vehicle_Milage = vehicle.Vehicle_Milage;
                    vehicleViewModel.Vehicle_Year = vehicle.Vehicle_Year;
                    vehicleViewModel.IsFileAvailable = vehicle.IsFileAvailable;
                    vehicleViewModel.BrandName = vehicle.BrandName;
                    adViewModel.VehicleViewModel = vehicleViewModel;
                    ViewBag.vehicle = true;

                }
                else if (adpost.fkPropertyId != null)
                {
                    Property property = db.Properties.Where(x => x.PropertyId == adpost.fkPropertyId).SingleOrDefault();
                    PropertyViewModel propertyViewModel = new PropertyViewModel();
                    propertyViewModel.PropertyId = property.PropertyId;
                    propertyViewModel.PropertyArea = property.PropertyArea;
                    propertyViewModel.WashRoom = property.WashRoom;
                    propertyViewModel.BedRoom = property.BedRoom;
                    propertyViewModel.IsCarParking = property.IsCarParking;
                    propertyViewModel.AreaType = property.AreaType;
                    adViewModel.PropertyViewModel = propertyViewModel;
                    ViewBag.property = true;
                }
                else if (adpost.fkBikeid != null)
                {
                    Bike bike = db.Bikes.Where(x => x.BikeID == adpost.fkBikeid).SingleOrDefault();
                    BikeViewModel bikeViewModel = new BikeViewModel();
                    bikeViewModel.BikeID = bike.BikeID;
                    bikeViewModel.BikeMileage = bike.BikeMileage;
                    bikeViewModel.BikeYear = bike.BikeYear;
                    bikeViewModel.BrandName = bike.BrandName;
                    bikeViewModel.IsFileAvailable = bike.IsFileAvailable;
                    adViewModel.BikeViewModel = bikeViewModel;
                    ViewBag.bike = true;
                }
                else if (adpost.fkClothid != null)
                {
                    Cloth cloth = db.Clothes.Where(x => x.ClothId == adpost.fkClothid).SingleOrDefault();
                    ClothViewModel clothViewModel = new ClothViewModel();
                    clothViewModel.ClothId = cloth.ClothId;
                    clothViewModel.ClothType = cloth.ClothType;
                    clothViewModel.Color = cloth.Color;
                    clothViewModel.Size = cloth.Size;
                    adViewModel.ClothViewModel = clothViewModel;
                    ViewBag.cloth = true;
                }
                else if (adpost.fkMobileId != null)
                {
                    Mobile mobile = db.Mobiles.Where(x => x.MobilesId == adpost.fkMobileId).SingleOrDefault();
                    MobileViewModel mobileViewModel = new MobileViewModel();
                    mobileViewModel.MobilesId = mobile.MobilesId;
                    mobileViewModel.BrandName = mobile.BrandName;
                    adViewModel.MobileViewModel = mobileViewModel;
                    ViewBag.mobile = true;
                }
                else if (adpost.fkElectronicId != null)
                {
                    Electronic electronic = db.Electronics.Where(x => x.ElectronicId == adpost.fkElectronicId).SingleOrDefault();
                    ElectronicViewModel electronicViewModel = new ElectronicViewModel();
                    electronicViewModel.ElectronicId = electronic.ElectronicId;
                    electronicViewModel.Brandname = electronic.BrandName;
                    electronicViewModel.ModelYear = electronic.ModelYear;
                    adViewModel.ElectronicViewModel = electronicViewModel;
                    ViewBag.electronic = true;
                }

                adViewModel.Cat_name = adpost.Category.cat_name;
                string currentUserId = User.Identity.GetUserId();

                //var u = db.Users.Where(x => x.Id == currentUserId).SingleOrDefault();
                var user = UserManager.FindById(adpost.User.Id);
                adViewModel.UserName = user.UserName;
                
                adViewModel.Contact = user.PhoneNumber;
                adViewModel.UserID = user.Id;
                return View(adViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }



    }
}