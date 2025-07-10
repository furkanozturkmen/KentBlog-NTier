using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class GetBlogViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;
        private readonly IAdminSettingsService _adminSettingsService;
        private readonly IThemeSettingsService _themeSettingsService;

        public GetBlogViewComponent(IBlogService blogService, IAdminSettingsService adminSettingsService, IThemeSettingsService themeSettingsService)
        {
            _blogService = blogService;
            _adminSettingsService = adminSettingsService;
            _themeSettingsService = themeSettingsService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _blogService.GetBlogsWithCategory();
            var admin = _adminSettingsService.GetListAll().FirstOrDefault();
            var theme = _themeSettingsService.GetListAll().FirstOrDefault();

            var viewmodel = new AdminBlogViewModel
            {
                blogs = values,
                AdminSettings = admin,
                ThemeSettings = theme
            };

            return View(viewmodel);
        }
    }
}
