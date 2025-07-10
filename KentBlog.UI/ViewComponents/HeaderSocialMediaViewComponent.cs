using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class HeaderSocialMediaViewComponent : ViewComponent
    {
        private readonly IGeneralSettingsService _generalsettingsService;

        public HeaderSocialMediaViewComponent(IGeneralSettingsService generalSettingsService)
        {
           _generalsettingsService = generalSettingsService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _generalsettingsService.GetListAll();
            return View(values);
        }
    }
}
