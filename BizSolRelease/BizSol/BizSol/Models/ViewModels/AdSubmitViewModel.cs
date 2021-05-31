using BizSol.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BizSol.Models.ViewModels
{
    
    public class VehicleViewModel : AdPostViewModel
    {
        public int Vehicle_Id { get; set; }
        [Required]
        [Range(1, 1000000000, ErrorMessage = "Value must be between 1 to 1000000000")]
        public int Vehicle_Milage { get; set; }
        [Required]
        [Range(1500, 3000, ErrorMessage = "Value must be between 1500 to 3000")]
        public int Vehicle_Year { get; set; }
        public string IsFileAvailable { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string BrandName { get; set; }
    }
    public class MobileViewModel : AdPostViewModel
    {
        public int MobilesId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string BrandName { get; set; }
    }
    public class PropertyViewModel : AdPostViewModel
    {
        public int PropertyId { get; set; }
        public string AreaType { get; set; }
        [Required]
        [Range(1, 2000, ErrorMessage = "Value must be between 1 to 2000")]
        public int PropertyArea { get; set; }
        [Required]
        public int WashRoom { get; set; }
        [Required]
        public int BedRoom { get; set; }
        public string IsCarParking { get; set; }
    }
    public class ClothViewModel : AdPostViewModel
    {
        public int ClothId { get; set; }
        [Required]
        public string ClothType { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public string Color { get; set; }
    }
    public class ElectronicViewModel : AdPostViewModel
    {
        public int ElectronicId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Brandname { get; set; }
        [Required]
        [Range(1500, 3000, ErrorMessage = "Value must be between 1500 to 3000")]
        public int ModelYear { get; set; }
    }
    public class BikeViewModel : AdPostViewModel
    {
        public int BikeID { get; set; }
        [Required]
        [Range(1, 1000000000, ErrorMessage = "Value must be between 1 to 1000000000")]
        public int BikeMileage { get; set; }
        [Required]
        [Range(1500, 3000, ErrorMessage = "Value must be between 1500 to 3000")]
        public int BikeYear { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string BrandName { get; set; }
        public string IsFileAvailable { get; set; }
    }
}