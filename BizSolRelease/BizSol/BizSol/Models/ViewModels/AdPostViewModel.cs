using BizSol.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BizSol.Models.ViewModels
{
    public class AdPostViewModel
    {
        public int PostId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        [Range(1, 1000000000, ErrorMessage = "Value must be between 1 to 1000000000")]
        public int Price { get; set; }
        public string Condition { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Detail { get; set; }
        public string UserID { get; set; }
        public List<AdsImages> AdsImages { get; set; }
        //public string Image { get; set; }
        //public Nullable<int> fk_vehicle_Id { get; set; }
        //public string Image1 { get; set; }
        //public string Image2 { get; set; }
        public Nullable<int> fkBikeid { get; set; }
        public Nullable<int> fkMobileId { get; set; }
        public Nullable<int> fkElectronicId { get; set; }
        public Nullable<int> fkClothid { get; set; }
        public Nullable<int> fkPropertyId { get; set; }
        //public System.DateTime WhenAdded { get; set; }
        public Nullable<int> FkCityId { get; set; }

        private DateTime _date = DateTime.Now;

        public DateTime WhenAdded
        {
            get { return _date; }
            set { _date = value; }
        }
        public int cat_Id { get; set; }
        public int CityID { get; set; }
    }
}