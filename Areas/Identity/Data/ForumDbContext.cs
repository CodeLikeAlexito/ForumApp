using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Areas.Identity.Data;
using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data
{
    public class ForumDbContext : IdentityDbContext<ForumUser>
    {
        private readonly DbContextOptions _options;
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
            _options = options;
        }

        //public DbSet<T> ObjectSet { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Replay> Replay { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
