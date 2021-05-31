using BizSol.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizSol.Models.ViewModels
{
    public class AdsViewModel
    {
        public int MaximumPrice { get; set; }
        public List<AdPost> adpost { get; set; }
        public List<City> cities { get; set; }
        public List<Category> categories { get; set; }
        public string searchTerm { get; set; }
        public int categoryId { get; set; }
        public string Condition { get; set; }
        public int cityId { get; set; }
        public int pageNo { get; set; }
        public Pager Pager { get; set; }
        public int? SortBy { get; set; }
        public int? SelectedCityId { get; set; }
    }
    public class FilterAdsViewModel
    {
        public List<AdPost> adpost{ get; set; }
        public int MaximumPrice { get; set; }
        public Pager Pager { get; set; }
        public int? SortBy { get; set; }
        public int categoryId { get; set; }
        //public int? CategoryID { get; set; }
        public string searchTerm { get; set; }
        public string Condition { get; set; }
        public int? SelectedCityId { get; set; }
    }
    public class SearchViewModel
    {
        public List<AdPost> adpost { get; set; }
        public List<City> cities { get; set; }
        public List<Category> categories { get; set; }
        public string searchTerm { get; set; }
    }
}