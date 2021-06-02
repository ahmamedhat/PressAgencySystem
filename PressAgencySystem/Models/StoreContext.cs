using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PressAgencySystem.Models
{ 
    public class StoreContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
    }
}