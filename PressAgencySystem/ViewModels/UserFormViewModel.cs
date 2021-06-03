using PressAgencySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PressAgencySystem.ViewModels
{
    public class UserFormViewModel
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}