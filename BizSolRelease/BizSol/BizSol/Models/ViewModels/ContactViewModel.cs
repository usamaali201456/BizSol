using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BizSol.Models.ViewModels
{
    public class ContactViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [MinLength(5), MaxLength(50)]
        public string FullName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Message is required")]
        [MinLength(20), MaxLength(200)]
        public string Message { get; set; }
        
        public string PhoneNumber { get; set; }
    }
}