using BizSol.Entities;
using BizSol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BizSol.Services
{
    public class AdServices
    {
        public static List<Category> GetCategory()
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
                IQueryable<AdPost> adpost = db.AdPosts.Where(x=>x.FkCityId == cityId).OrderByDescending(x => x.WhenAdded).AsQueryable();

                if (selectedCategoryId.HasValue)
                {
                    adpost = adpost.Where(x => x.cat_Id == selectedCategoryId.Value).OrderByDescending(x => x.WhenAdded);
                }
                if (selectedCityId.HasValue)
                {
                    adpost = adpost.Where(x => x.FkCityId == selectedCityId).OrderByDescending(x => x.WhenAdded);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    adpost = adpost.Where(x => x.Title.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.WhenAdded);
                }

                if (minimumPrice.HasValue)
                {
                    adpost = adpost.Where(x => x.Price >= minimumPrice.Value).OrderByDescending(x => x.WhenAdded);
                }

                if (maximumPrice.HasValue)
                {
                    adpost = adpost.Where(x => x.Price <= maximumPrice.Value).OrderByDescending(x => x.WhenAdded);
                }

               
                return adpost.ToList();
                //return adpost.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public static List<AdPost> CategorySearchAd(string search, int? minimumPrice, int? maximumPrice, int? selectedCategoryId, int? selectedCityId, int? categoryId, int? sortBy, string condition, int pageNo, int pageSize)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<AdPost> adpost = db.AdPosts.Include(img=>img.AdsImages).Where(x => x.cat_Id == categoryId).OrderByDescending(x=>x.WhenAdded).AsQueryable();

                if (selectedCityId.HasValue)
                {
                    adpost = adpost.Where(x => x.FkCityId == selectedCityId.Value).OrderByDescending(x => x.WhenAdded);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    adpost = adpost.Where(x => x.Title.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.WhenAdded);
                }

                if (!string.IsNullOrEmpty(condition) && categoryId != 0)
                {
                    adpost = adpost.Where(x => x.Condition.ToLower().Contains(condition.ToLower())).OrderByDescending(x => x.WhenAdded);
                }

                if (minimumPrice.HasValue)
                {
                    adpost = adpost.Where(x => x.Price >= minimumPrice.Value).OrderByDescending(x => x.WhenAdded);
                }

                if (maximumPrice.HasValue)
                {
                    adpost = adpost.Where(x => x.Price <= maximumPrice.Value).OrderByDescending(x => x.WhenAdded);
                }
                
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            adpost = adpost.OrderBy(x => x.WhenAdded);
                            break;
                        case 3:
                            adpost = adpost.OrderBy(x => x.Price);
                            break;
                        case 4:
                            adpost = adpost.OrderByDescending(x => x.Price);
                            break;
                        default:
                            adpost = adpost.OrderByDescending(x => x.WhenAdded);
                            break;
                    }
                }

               
                return adpost.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                //return adpost.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public static int CategorySearchAdCount(string search, int? minimumPrice, int? maximumPrice, int? selectedCategoryId, int? selectedCityId, int? categoryId, int? sortBy, string condition)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<AdPost> adpost = db.AdPosts.Include(i=>i.AdsImages).Where(x => x.cat_Id == categoryId).OrderByDescending(x => x.WhenAdded).AsQueryable();

                if (selectedCityId.HasValue)
                {
                    adpost = adpost.Where(x => x.FkCityId == selectedCityId.Value).OrderByDescending(x => x.WhenAdded);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    adpost = adpost.Where(x => x.Title.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.WhenAdded);
                }

                if (!string.IsNullOrEmpty(condition) && categoryId != 0)
                {
                    adpost = adpost.Where(x => x.Condition.ToLower().Contains(condition.ToLower())).OrderByDescending(x => x.WhenAdded);
                }

                if (minimumPrice.HasValue)
                {
                    adpost = adpost.Where(x => x.Price >= minimumPrice.Value).OrderByDescending(x => x.WhenAdded);
                }

                if (maximumPrice.HasValue)
                {
                    adpost = adpost.Where(x => x.Price <= maximumPrice.Value).OrderByDescending(x => x.WhenAdded);
                }
               
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            adpost = adpost.OrderBy(x => x.WhenAdded);
                            break;
                        case 3:
                            adpost = adpost.OrderBy(x => x.Price);
                            break;
                        case 4:
                            adpost = adpost.OrderByDescending(x => x.Price);
                            break;
                        default:
                            adpost = adpost.OrderByDescending(x => x.WhenAdded);
                            break;
                    }
                }

                return adpost.ToList().Count;
            }
        }

        public static int GetMaximumPrice(int? categoryId)/*int pageNo, string searchTerm*/
        {
            using (var db = new ApplicationDbContext())
            {
                return (db.AdPosts.Where(x=>x.cat_Id == categoryId).Max(x => x.Price));
                
            }
        }

    }
}