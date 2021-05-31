using BizSol.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizSol.Models.ViewModels
{
    public class AdViewModel
    {
        public Nullable<int> CityID { get; set; }
        public string CityName { get; set; }
        public string CityImage { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Condition { get; set; }
        public string Detail { get; set; }
        public List<AdsImages> AdsImages { get; set; }

        public VehicleViewModel VehicleViewModel { get; set; }
        public MobileViewModel MobileViewModel { get; set; }
        public PropertyViewModel PropertyViewModel { get; set; }
        public ClothViewModel ClothViewModel { get; set; }
        public ElectronicViewModel ElectronicViewModel { get; set; }
        public BikeViewModel BikeViewModel { get; set; }

        public string UserID { get; set; }
        public System.DateTime WhenAdded { get; set; }

        public int Cat_Id { get; set; }
        public string Cat_name { get; set; }
        public string UserName { get; set; }
        public string Contact { get; set; }

    }
}