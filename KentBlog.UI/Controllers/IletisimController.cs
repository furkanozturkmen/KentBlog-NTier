using KentBlog.Business.Abstract;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Controllers
{
    public class IletisimController : Controller
    {
        private readonly IGeneralSettingsService _generalSettingsService;
        private readonly IAdminSettingsService _adminSettingsService;
        private readonly IMenuService _menuService;

        public IletisimController(IGeneralSettingsService generalSettingsService, IAdminSettingsService adminSettingsService, IMenuService menuService)
        {
            _generalSettingsService = generalSettingsService;
            _adminSettingsService = adminSettingsService;
            _menuService = menuService;
        }


        public IActionResult Index()
        {
            var admin = _adminSettingsService.GetListAll().FirstOrDefault();
            var general = _generalSettingsService.GetListAll().FirstOrDefault();
            var menu = _menuService.GetListAll().Where(m => m.MenuKey == "iletisim").FirstOrDefault();

            ViewData["PageTitle"] = "İletişim";

            var viewmodel = new ContactViewModel { AdminSettings = admin , GeneralSettings = general, Menu = menu};

            return View(viewmodel);
        }
    }
}
