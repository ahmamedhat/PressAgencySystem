using PressAgencySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PressAgencySystem.ViewModels
{
    public class PostFormViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<ArticleType> articleTypes { get; set; }
    }
}