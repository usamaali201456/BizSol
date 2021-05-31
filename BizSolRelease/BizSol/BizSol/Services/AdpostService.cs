using BizSol.Entities;
using BizSol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizSol.Services
{
    public class AdpostService
    {
        public static List<AdPost> GetAds(int pageNo,string searchTerm)
        {
            int pageSize = 12;
            using (var db = new ApplicationDbContext())
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    return db.AdPosts.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower())).OrderByDescending(x => x.WhenAdded).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                }
                else
                { 
                    return db.AdPosts.OrderByDescending(x => x.WhenAdded).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                }
            }
        }
       
    }
}