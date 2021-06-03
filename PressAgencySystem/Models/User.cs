using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PressAgencySystem.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Email is Required")]
        [Display(Name = "Email")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "UserName is Required")]
        [Display(Name ="User Name")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Display(Name ="User Role")]
        public int RoleId { get; set; }
        [Display(Name = "User Role")]

        public Role Role { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [Display(Name = "Password")]
        [StringLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}