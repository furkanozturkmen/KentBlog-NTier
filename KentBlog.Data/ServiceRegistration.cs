using KentBlog.Data.Abstract;
using KentBlog.Data.Concrete;
using KentBlog.Data.Concrete.EfCore;
using KentBlog.Entity.Concrete;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;




namespace KentBlog.Data
{
    public static class ServiceRegistration
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAdminSettingsRepository, EfAdminSettingsRepository>();
            services.AddScoped<IBlogRepository, EfBlogRepository>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();
            services.AddScoped<IContactRepository, EfContactRepository>();
            services.AddScoped<IGeneralSettingsRepository, EfGeneralSettingsRepository>();
            services.AddScoped<IKeywordsRepository, EfKeywordsRepository>();
            services.AddScoped<IMenuRepository, EfMenuRepository>();
            services.AddScoped<IOpeningPageRepository, EfOpeningPageRepository>();
            services.AddScoped<ISliderRepository, EfSliderRepository>();
            services.AddScoped<IThemeSettingsRepository, EfThemeSettingsRepository>();
            services.AddScoped<IVisitorRepository, EfVisitorRepository>();
            services.AddScoped<ICounterRepository, EfCounterRepository>();
            services.AddScoped<IServicesRepository, EfServicesRepository>();
            services.AddScoped<IAboutUsRepository, EfAboutUsRepository>();
            services.AddScoped<ITestimonialsRepository, EfTestimonialsRepository>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(EfGenericRepository<>));
        } 
    }
}
