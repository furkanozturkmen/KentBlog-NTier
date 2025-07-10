using KentBlog.Business.Abstract;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class WhatsappPhoneViewComponent : ViewComponent
    {

        private readonly IAdminSettingsService _adminSettingsService;
        private readonly IGeneralSettingsService _generalSettingsService;

        public WhatsappPhoneViewComponent(IAdminSettingsService adminSettingsService, IGeneralSettingsService generalSettingsService)
        {
            _adminSettingsService = adminSettingsService;
            _generalSettingsService = generalSettingsService;
        }
        public IViewComponentResult Invoke()
        {

            var admin = _adminSettingsService.GetListAll().FirstOrDefault();
            var general = _generalSettingsService.GetListAll();

            var viewmodel = new AdminGeneralViewModel
            {
                AdminSettings = admin,
                GeneralSettings = general
            };

            return View(viewmodel);
        }
    }
}
