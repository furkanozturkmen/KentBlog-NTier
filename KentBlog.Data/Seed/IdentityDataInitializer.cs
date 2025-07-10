using Azure;
using KentBlog.Data.Concrete;
using KentBlog.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KentBlog.Data.Seed
{
    public class IdentityDataInitializer
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Editor" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }



        public static async Task SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            var defaultUser = new ApplicationUser
            {
                FullName = "Admin Kullanıcı",
                UserName = "webmaster",
                Email = "webmaster@kentmedia.com.tr",
            };

            string password = "KntBlg05!";

            var user = await userManager.FindByNameAsync(defaultUser.UserName);
            if (user == null)
            {
                var result = await userManager.CreateAsync(defaultUser, password);
                if (result.Succeeded)
                {
                    string roleName = "Admin";
                    await userManager.AddToRoleAsync(defaultUser, roleName);
                }
                else
                {

                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Hata: {error.Description}");
                    }
                }
            }
        }





        public static async Task SeedMainPages(Context context)
        {
            var openingPages = new List<OpeningPage>
            {
                new OpeningPage
                {
                    Name = "Haberler",
                    SectionKey = "blog"
                },
                new OpeningPage
                {
                    Name = "Kategoriler",
                    SectionKey = "category"
                },
                new OpeningPage
                {
                    Name = "Anahtar Kelime",
                    SectionKey = "keywords"
                },
                new OpeningPage
                {
                    Name = "Hakkımızda",
                    SectionKey = "aboutus"
                },
                new OpeningPage
                {
                    Name = "Sayaç",
                    SectionKey = "counter"
                },
                new OpeningPage
                {
                    Name = "Hizmet",
                    SectionKey = "services"
                },
                new OpeningPage
                {
                    Name = "Yorum",
                    SectionKey = "testimonial"
                }

            };


            foreach (var page in openingPages)
            {
                bool exists = await context.OpeningPage
                    .AnyAsync(x => x.SectionKey == page.SectionKey);

                if (!exists)
                {
                    context.OpeningPage.Add(page);
                }
            }

            await context.SaveChangesAsync();
        }


        public static async Task SeedMenu(Context context)
        {
            var menus = new List<Menu>
            {
                new Menu
                {
                    Name = "Anasayfa",
                    MenuKey = "homepage"
                },
                new Menu
                {
                    Name = "Kurumsal",
                    MenuKey = "kurumsal"
                },
                new Menu
                {
                    Name = "Haberler",
                    MenuKey = "blog"
                },
                new Menu
                {
                    Name = "Kategoriler",
                    MenuKey = "category"
                },
                new Menu
                {
                    Name = "İletişim",
                    MenuKey = "iletisim"
                }

            };


            foreach (var page in menus)
            {
                bool exists = await context.Menus
                    .AnyAsync(x => x.MenuKey == page.MenuKey);

                if (!exists)
                {
                    context.Menus.Add(page);
                }
            }

            await context.SaveChangesAsync();
        }



        public static async Task SeedAdminSettings(Context context)
        {

            if (!context.AdminSettings.Any())
            {
                var settings = new List<AdminSettings>
                {
                    new AdminSettings
                    {
                        FooterInfo = "<p><a style=\"font-weight: bold;\" href=\"https://www.kentmedia.com.tr\">Web Tasarım</a> Kent Media</p>"
                    }
                };

                context.AdminSettings.AddRange(settings);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedThemeSettings(Context context)
        {

            if (!context.ThemeSettings.Any())
            {
                var settings = new List<ThemeSettings>
                {
                    new ThemeSettings
                    {
                        BlogColumn = "4",
                        CategoryType = "1",
                        BlogType = "1"
                    }
                };

                context.ThemeSettings.AddRange(settings);
                await context.SaveChangesAsync();
            }
        }



        public static async Task SeedGeneralSettings(Context context)
        {

            if (!context.GeneralSettings.Any())
            {
                var settings = new List<GeneralSettings>
                {
                    new GeneralSettings
                    {
                        AboutUs = null
                    }
                };

                context.GeneralSettings.AddRange(settings);
                await context.SaveChangesAsync();
            }
        }



    }
}