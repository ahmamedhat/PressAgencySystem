using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PressAgencySystem.Models
{ 
    public class StoreContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        internal DbRawSqlQuery<Post> Database(string v)
        {
            throw new NotImplementedException();
        }
    }
}