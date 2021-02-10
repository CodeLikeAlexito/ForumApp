using System;
using Forum.Areas.Identity.Data;
using Forum.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Forum.Areas.Identity.IdentityHostingStartup))]
namespace Forum.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            /*
            builder.ConfigureServices((context, services) => {
                //services.AddDbContextPool<ForumDbContext>(options =>
                //    options.UseMySql(
                //        context.Configuration.GetConnectionString("ForumDbContextConnection")));
                /*
                services.AddDbContextPool<ForumDbContext>(opitons => opitons.UseMySql(context.Configuration.GetConnectionString("ForumDbContextConnection")));

                services.AddDefaultIdentity<ForumUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })

                    .AddEntityFrameworkStores<ForumDbContext>();
            });*/
        }
    }
}