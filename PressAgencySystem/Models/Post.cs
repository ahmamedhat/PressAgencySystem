using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PressAgencySystem.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Title is Required")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        [Display(Name ="Type")]
        public int ArticleTypeId { get; set; }
        [Display(Name = "Article Type")]
        public ArticleType ArticleType { get; set; }
    }
}