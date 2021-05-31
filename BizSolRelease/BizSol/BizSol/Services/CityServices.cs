using BizSol.Entities;
using BizSol.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizSol.Services
{
    public class CityServices
    {

        public static List<AdPost> GetAds()/*int pageNo, string searchTerm*/
        {
            //int pageSize = 12;
            using (var db = new ApplicationDbContext())
            {
                return db.AdPosts.ToList(); /*Include(x => x)*/
                
            }
        }
        public static int GetAdCount()/*int pageNo, string searchTerm*/
        {
            using (var db = new ApplicationDbContext())
            {
                return db.AdPosts.Count();

            }
        }
        public static List<Category> GetCategory()/*int pageNo, string searchTerm*/
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Categories.ToList();

            }
        }

        internal static List<City> GetCity()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Cities.ToList();

            }
        }

        public static List<AdPost> CitySearchAd(string search, int? minimumPrice, int? maximumPrice, int? selectedCategoryId, int? selectedCityId, int? cityId)
        {
            using (var db = new ApplicationDbContext())
            {
                var adpost = db.AdPosts.Where(x => x.FkCityId == cityId).OrderByDescending(x => x.WhenAdded).ToList();

                if (selectedCategoryId.HasValue)
                {
                    adpost = adpost.Where(x => x.cat_Id == selectedCategoryId.Value).OrderByDescending(x => x.WhenAdded).ToList();
                }
                if (selectedCityId.HasValue)
                {
                    adpost = adpost.Where(x => x.FkCityId == selectedCityId).OrderByDescending(x => x.WhenAdded).ToList();
                }
                if (!string.IsNullOrEmpty(search))
                {
                    adpost = adpost.Where(x => x.Title.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.WhenAdded).ToList();
                }

                if (minimumPrice.HasValue)
                {
                    adpost = adpost.Where(x => x.Price >= minimumPrice.Value).OrderByDescending(x => x.WhenAdded).ToList();
                }

                if (maximumPrice.HasValue)
                {
                    adpost = adpost.Where(x => x.Price <= maximumPrice.Value).OrderByDescending(x => x.WhenAdded).ToList();
                }

                //if (sortBy.HasValue)
                //{
                //    switch (sortBy.Value)
                //    {
                //        case 2:
                //            products = products.OrderByDescending(x => x.ID).ToList();
                //            break;
                //        case 3:
                //            products = products.OrderBy(x => x.Price).ToList();
                //            break;
                //        default:
                //            products = products.OrderByDescending(x => x.Price).ToList();
                //            break;
                //    }
                //}
                return adpost;
                //return adpost.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public static List<AdPost> CitySearchAd(string search, int? minimumPrice, int? maximumPrice, int? selectedCategoryId, int? selectedCityId, int? cityId, int? sortBy, string condition, int pageNo, int pageSize)
        {
            using (var db = new ApplicationDbContext())
            {
                var adpost = db.AdPosts.Include(im=>im.AdsImages).Where(x => x.FkCityId == cityId).OrderByDescending(x => x.WhenAdded).ToList();

                if (selectedCategoryId.HasValue)
                {
                    adpost = adpost.Where(x => x.cat_Id == selectedCategoryId.Value).OrderByDescending(x => x.WhenAdded).ToList();
                }

                if (!string.IsNullOrEmpty(search))
                {
                    adpost = adpost.Where(x => x.Title.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.WhenAdded).ToList();
                }

                if (!string.IsNullOrEmpty(condition) && cityId != 0)
                {
                    adpost = adpost.Where(x => x.Condition.ToLower().Contains(condition.ToLower())).OrderByDescending(x => x.WhenAdded).ToList();
                }
                if (minimumPrice.HasValue && maximumPrice.HasValue)
                {
                    adpost = adpost.Where(x => x.Price >= minimumPrice && x.Price <= maximumPrice).OrderByDescending(x => x.WhenAdded).ToList();
                }

                //if (minimumPrice.HasValue)
                //{
                //    adpost = adpost.Where(x => x.Price >= minimumPrice.Value).OrderByDescending(x => x.WhenAdded).ToList();
                //}

                //if (maximumPrice.HasValue)
                //{
                //    adpost = adpost.Where(x => x.Price <= maximumPrice.Value).OrderByDescending(x => x.WhenAdded).ToList();
                //}
                //var sort = (SortByEnums)sortBy.Value
                //  switch (sort)
                //{
                //    case SortByEnums.Default:
                //        break;
                //    case SortByEnums.Popularity:
                //        break;
                //    case SortByEnums.PriceLowToHigh:
                //        break;
                //    case SortByEnums.PriceHighToLow:
                //        break;
                //    default:
                //        break;
                //}
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            adpost = adpost.OrderBy(x => x.WhenAdded).ToList();
                            break;
                        case 3:
                            adpost = adpost.OrderBy(x => x.Price).ToList();
                            break;
                        case 4:
                            adpost = adpost.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            adpost = adpost.OrderByDescending(x => x.WhenAdded).ToList();
                            break;
                    }
                }

                //if (sortBy.HasValue)
                //{
                //    switch (sortBy.Value)
                //    {
                //        case 2:
                //            products = products.OrderByDescending(x => x.ID).ToList();
                //            break;
                //        case 3:
                //            products = products.OrderBy(x => x.Price).ToList();
                //            break;
                //        default:
                //            products = products.OrderByDescending(x => x.Price).ToList();
                //            break;
                //    }
                //}
                return adpost.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                //return adpost.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public static int CitySearchAdCount(string search, int? minimumPrice, int? maximumPrice, int? selectedCategoryId, int? selectedCityId, int? cityId, int? sortBy, string condition)
        {
            using (var db = new ApplicationDbContext())
            {
                var adpost = db.AdPosts.Include(im=>im.AdsImages).Where(x => x.FkCityId == cityId).OrderByDescending(x => x.WhenAdded).ToList();

                if (selectedCategoryId.HasValue)
                {
                    adpost = adpost.Where(x => x.cat_Id == selectedCategoryId.Value).OrderByDescending(x => x.WhenAdded).ToList();
                }
                if (minimumPrice.HasValue && maximumPrice.HasValue)
                {
                    adpost = adpost.Where(x => x.Price >= minimumPrice && x.Price <= maximumPrice).OrderByDescending(x => x.WhenAdded).ToList();
                }

                if (!string.IsNullOrEmpty(search))
                {
                    adpost = adpost.Where(x => x.Title.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.WhenAdded).ToList();
                }

                if (!string.IsNullOrEmpty(condition) && cityId != 0)
                {
                    adpost = adpost.Where(x => x.Condition.ToLower().Contains(condition.ToLower())).OrderByDescending(x => x.WhenAdded).ToList();
                }

                //if (minimumPrice.HasValue)
                //{
                //    adpost = adpost.Where(x => x.Price >= minimumPrice.Value).OrderByDescending(x => x.WhenAdded).ToList();
                //}

                //if (maximumPrice.HasValue)
                //{
                //    adpost = adpost.Where(x => x.Price <= maximumPrice.Value).OrderByDescending(x => x.WhenAdded).ToList();
                //}
                //var sort = (SortByEnums)sortBy.Value
                //  switch (sort)
                //{
                //    case SortByEnums.Default:
                //        break;
                //    case SortByEnums.Popularity:
                //        break;
                //    case SortByEnums.PriceLowToHigh:
                //        break;
                //    case SortByEnums.PriceHighToLow:
                //        break;
                //    default:
                //        break;
                //}
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            adpost = adpost.OrderBy(x => x.WhenAdded).ToList();
                            break;
                        case 3:
                            adpost = adpost.OrderBy(x => x.Price).ToList();
                            break;
                        case 4:
                            adpost = adpost.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            adpost = adpost.OrderByDescending(x => x.WhenAdded).ToList();
                            break;
                    }
                }

                return adpost.Count;
            }
        }

        public static int GetMaximumPrice(int? categoryId)/*int pageNo, string searchTerm*/
        {
            using (var db = new ApplicationDbContext())
            {
                return (int)(db.AdPosts.Where(x => x.cat_Id == categoryId).Max(x => x.Price));
             
            }
        }
    }
}