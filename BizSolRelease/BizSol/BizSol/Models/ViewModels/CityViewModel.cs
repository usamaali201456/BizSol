
using BizSol.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizSol.Models.ViewModels
{
    public class CityViewModel
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
        public int? SelectedCategoryId { get;  set; }
    }
    public class CityFilterViewModel
    {
        public List<AdPost> adpost { get; set; }
        public int MaximumPrice { get; set; }
        public Pager Pager { get; set; }
        public int? SortBy { get; set; }
        public int categoryId { get; set; }
        //public int? CategoryID { get; set; }
        public string searchTerm { get; set; }
        public int cityId { get; set; }
        public string Condition { get; set; }
        public int? SelectedCityId { get; set; }

        public int? SelectedCategoryId { get; set; }
    }
}