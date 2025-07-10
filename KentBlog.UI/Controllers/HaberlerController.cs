using KentBlog.Business.Abstract;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Controllers
{
    public class HaberlerController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IMenuService _menuService;
        private readonly IAdminSettingsService _adminSettingsService;
        public HaberlerController(IBlogService blogService, IMenuService menuService, IAdminSettingsService adminSettingsService)
        {
            _blogService = blogService;
            _menuService = menuService;
            _adminSettingsService = adminSettingsService;
        }
        public IActionResult Index()
        {

            ViewData["PageTitle"] = "Tüm Haberler";
            ViewData["IsMenuPage"] = true;

            var values = _blogService.GetBlogsWithCategory();
            var menu = _menuService.GetListAll().Where(m => m.MenuKey == "blog").FirstOrDefault();
            var admin = _adminSettingsService.GetListAll().FirstOrDefault();

            var viewmodel = new BlogMenuViewModel
            {
                blogs = values,
                Menu = menu, 
                AdminSettings = admin,
            };

            return View(viewmodel);
        }
    }
}
