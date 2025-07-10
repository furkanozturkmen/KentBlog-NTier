using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KentBlog.UI.ViewComponents
{
    public class FaviconViewComponent : ViewComponent
    {

        private readonly IAdminSettingsService _adminSettingsService;

        public FaviconViewComponent(IAdminSettingsService adminSettingsService)
        {
            _adminSettingsService = adminSettingsService;
        }


        public IViewComponentResult Invoke()
        {
            var values = _adminSettingsService.GetListAll().FirstOrDefault();
            return View(values);
        }

    }
}
