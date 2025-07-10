using KentBlog.Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Data.Concrete
{
    public class Context : IdentityDbContext<ApplicationUser>
    {

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }


        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<AdminSettings> AdminSettings { get; set; }
        public DbSet<GeneralSettings> GeneralSettings { get; set; }
        public DbSet<ThemeSettings> ThemeSettings { get; set; }
        public DbSet<Keywords> Keywords { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<OpeningPage> OpeningPage { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Testimonials> Testimonials { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Counter> Counters { get; set; }




    }
}
