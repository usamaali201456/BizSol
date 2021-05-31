
using BizSol.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizSol.Models.ViewModels
{
    public class HomeViewModel
    {
        public int MaximumPrice { get; set; }
        public List<AdPost> adpost { get; set; }
        public List<AdPost> totalAds { get; set; }
        public List<City> cities { get; set; }
        public List<Category> categories { get; set; }
        public string searchTerm { get; set; }
        public int? CategoryId { get; set; }
        public string Condition { get; set; }
        public int pageNo { get; set; }
        public Pager Pager { get; set; }
        public int? SortBy { get; set; }
        public int? CityId { get; set; }
    }
    public class SearchHomeViewModel
    {
        public List<AdPost> adpost { get; set; }
        public int MaximumPrice { get; set; }
        public Pager Pager { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryId { get; set; }
        //public int? CategoryID { get; set; }
        public string searchTerm { get; set; }
        public string Condition { get; set; }
        public int? CityId { get; set; }
    }
}