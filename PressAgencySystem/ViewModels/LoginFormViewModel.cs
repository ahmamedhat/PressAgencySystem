using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PressAgencySystem.ViewModels
{
    public class LoginFormViewModel
    {
        [Required(ErrorMessage = "UserName is Required")]
        [Display(Name = "User Name")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [Display(Name = "Password")]
        [StringLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}