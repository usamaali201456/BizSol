using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BizSol.Models.ViewModels
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage ="New password is required",AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm password is required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Passowrd not matched")]
        public string ConnfirmPassword { get; set; }
        [Required]
        public string ResetCode { get; set; }
    }
}