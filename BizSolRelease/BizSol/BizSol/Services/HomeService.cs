using BizSol.Entities;
using BizSol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BizSol.Services
{
    public class HomeService
    {
        public static List<AdPost> CategorySearchAd(string search,  int? CityId, int? categoryId,  int pageNo, int pageSize)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<AdPost> adpost = db.AdPosts.Include(i => i.AdsImages).OrderByDescending(x => x.WhenAdded).AsQueryable();

                if (CityId.HasValue)
                {
                    adpost = adpost.Where(x => x.FkCityId == CityId.Value).OrderByDescending(x => x.WhenAdded);
                }
                if (categoryId.HasValue)
                {
                    adpost = adpost.Where(x => x.cat_Id == categoryId.Value).OrderByDescending(x => x.WhenAdded);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    adpost = adpost.Where(x => x.Title.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.WhenAdded);
                }
                              
                return adpost.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public static List<City> GetCity()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Cities.ToList();

            }
        }

        public static int CategorySearchAdCount(string search, int? CityId, int? categoryId)
        {
            using (var db = new ApplicationDbContext())
            {
                var adpost = db.AdPosts.OrderByDescending(x => x.WhenAdded).ToList();

                if (CityId.HasValue)
                {
                    adpost = adpost.Where(x => x.FkCityId == CityId.Value).OrderByDescending(x => x.WhenAdded).ToList();
                }
                if (categoryId.HasValue)
                {
                    adpost = adpost.Where(x => x.cat_Id == categoryId.Value).OrderByDescending(x => x.WhenAdded).ToList();
                }
                if (!string.IsNullOrEmpty(search))
                {
                    adpost = adpost.Where(x => x.Title.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.WhenAdded).ToList();
                }
                return adpost.Count;
            }
        }

    }
}