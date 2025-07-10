using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class MainNavViewComponent : ViewComponent
    {
        private readonly IMenuService _menuService;

        public MainNavViewComponent(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _menuService.GetListAll().OrderBy(m => m.Order).ToList();
            var menucount = values.Count;
            ViewBag.Menucount = menucount;
            return View(values);
        }
    }
}
