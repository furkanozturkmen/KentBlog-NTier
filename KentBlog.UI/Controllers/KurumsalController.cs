using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Controllers
{
    public class KurumsalController : Controller
    {

        private readonly IGeneralSettingsService _generalSettingsService;
        private readonly IMenuService _menuService;

        public KurumsalController(IGeneralSettingsService generalSettingsService, IMenuService menuService)
        {
            _generalSettingsService = generalSettingsService;
            _menuService = menuService;
        }


        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Kurumsal";
            ViewData["IsMenuPage"] = true;

            var values = _generalSettingsService.GetListAll().FirstOrDefault();
            var  menu = _menuService.GetListAll().Where(m => m.MenuKey == "kurumsal").FirstOrDefault();

            var viewmodel = new KurumsalMenuViewModel
            {
                GeneralSettings = values,
                Menu = menu
            };

            return View(viewmodel);
        }
    }
}
