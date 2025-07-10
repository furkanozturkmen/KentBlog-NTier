using KentBlog.Business.Abstract;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Controllers
{
    public class KategorilerController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMenuService _menuService;

        public KategorilerController(ICategoryService categoryService, IMenuService menuService)
        {
            _categoryService = categoryService;
            _menuService = menuService;
        }

        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Tüm Kategoriler";
            ViewData["IsMenuPage"] = true;

            var menu = _menuService.GetListAll().Where(m => m.MenuKey == "category").FirstOrDefault();
            var values = _categoryService.GetListAll();

            var viewmodel = new CategoryMenuViewModel
            {
                Category = values,
                Menu = menu
            };

            return View(viewmodel);
        }
    }
}
 