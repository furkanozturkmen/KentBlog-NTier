using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace KentBlog.Business
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services) 
        {

            services.AddScoped<IAdminSettingsService, AdminSettingsManager>();
            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IGeneralSettingsService, GeneralSettingsManager>();
            services.AddScoped<IKeywordsService, KeywordsManager>();
            services.AddScoped<IMenuService, MenuManager>();
            services.AddScoped<IOpeningPageService, OpeningPageManager>();
            services.AddScoped<ISliderService, SliderManager>();
            services.AddScoped<IThemeSettingsService, ThemeSettingsManager>();
            services.AddScoped<IVisitorService, VisitorManager>();
            services.AddScoped<ICounterService, CounterManager>();
            services.AddScoped<IServicesService, ServicesManager>();
            services.AddScoped<IAboutUsService, AboutUsManager>();
            services.AddScoped<ITestimonialsService, TestimonialsManager>();
        }
    }
}
