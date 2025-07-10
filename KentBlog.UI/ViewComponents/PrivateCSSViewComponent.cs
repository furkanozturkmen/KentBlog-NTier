using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class PrivateCSSViewComponent : ViewComponent
    {
        private readonly IThemeSettingsService _themeSettingsService;

        public PrivateCSSViewComponent(IThemeSettingsService themeSettingsService)
        {
            _themeSettingsService = themeSettingsService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _themeSettingsService.GetListAll().FirstOrDefault();
            return View(values);
        }
    }
}
