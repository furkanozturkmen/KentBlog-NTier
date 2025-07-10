using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class HeaderContactViewComponent : ViewComponent
    {
        private readonly IGeneralSettingsService _generalSettingsService;
        private readonly IAdminSettingsService _adminSettingsService;

        public HeaderContactViewComponent(IGeneralSettingsService generalsettingsService, IAdminSettingsService adminSettingsService)
        {
            _generalSettingsService = generalsettingsService;
            _adminSettingsService = adminSettingsService;   
        }

        public IViewComponentResult Invoke()
        {
            var generalsettings = _generalSettingsService.GetListAll();
            var adminsettings = _adminSettingsService.GetListAll().FirstOrDefault();

            var viewmodel = new AdminGeneralViewModel
            {
                AdminSettings = adminsettings,
                GeneralSettings = generalsettings
            };

            return View(viewmodel);

        }
    }
}
