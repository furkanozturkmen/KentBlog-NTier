using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using KentBlog.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace KentBlog.UI.Controllers
{
    public class IcerikController : Controller
    {

        private readonly IMenuService _menuService;

        public IcerikController(IMenuService menuService) 
        {
            _menuService = menuService;
        }

        [Route("icerik/{id:int}/{seo}")]
        public IActionResult Index(int id, string seo)
        {

            var menu = _menuService.GetByID(id);

            if (menu == null || !menu.MenuStatus)
            {
                return NotFound();
            }

            ViewData["IsMenuPage"] = true;
            ViewData["PageTitle"] = menu.SeoTitle;

            if (menu == null)
                return NotFound();

            var dogruSeo = Helpers.UrlDostuYap(menu.SeoTitle);
            if (seo != dogruSeo)
            {
                return RedirectToAction("Index", new { id = id, seo = dogruSeo });
            }

            return View(menu);
        }
    }
}
