using FluentValidation;
using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using KentBlog.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KentBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MenuController : Controller
    {

        private readonly IMenuService _menuService;
        private readonly IValidator<Menu> _validator;

        public MenuController(IMenuService menuService, IValidator<Menu> validator)
        {
            _menuService = menuService;
            _validator = validator;
        }


        public IActionResult Index()
        {

            var values = _menuService.GetListAll().OrderBy(m => m.Order).ToList();
            var menucount = values.Count;
            ViewBag.Menucount = menucount;

            var menus = _menuService.GetListAll();
            ViewBag.Menus = new SelectList(menus, "MenuID", "Name");
            ViewBag.MenuDictionary = menus.ToDictionary(m => m.MenuID, m => m.Name);


            return View(values);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            var menu = _menuService.GetByID(id);
            if (menu == null)
            {
                TempData["ErrorMessage"] = "Menü bulunamadı.";
                return RedirectToAction("Menu");
            }

            var menus = _menuService.GetListAll();
            ViewBag.Menus = new SelectList(menus, "MenuID", "Name");

            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Menu menu)
        {
            var result = _validator.Validate(menu);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(menu);
            }

            var existingMenu = _menuService.GetByID(menu.MenuID);

            if (existingMenu != null)
            {
                // Bağlı olduğu menü değiştiyse order güncelle
                bool submenuChanged = existingMenu.SubMenuSelect != menu.SubMenuSelect;

                existingMenu.PageType = menu.PageType;
                existingMenu.SubMenuSelect = menu.SubMenuSelect;
                existingMenu.MenuAdres = menu.MenuAdres;
                existingMenu.Name = menu.Name;
                existingMenu.MenuInfo = menu.MenuInfo;
                existingMenu.MenuStatus = menu.MenuStatus;
                existingMenu.MenuHide = menu.MenuHide;
                existingMenu.MenuChecked = menu.MenuChecked;
                existingMenu.MenuTarget = menu.MenuTarget;
                existingMenu.SeoTitle = menu.SeoTitle;
                existingMenu.MetaDescription = menu.MetaDescription;
                existingMenu.MetaKeyword = menu.MetaKeyword;

                if (submenuChanged)
                {
                    // Yeni bağlı olunan menünün order'ını al
                    if (!string.IsNullOrEmpty(menu.SubMenuSelect))
                    {
                        var parentMenu = _menuService.GetByID(Convert.ToInt32(menu.SubMenuSelect));
                        if (parentMenu != null)
                        {
                            int newOrder = parentMenu.Order + 1;

                            var menusToShift = _menuService.GetListAll()
                                .Where(m => m.Order >= newOrder && m.MenuID != existingMenu.MenuID)
                                .OrderByDescending(m => m.Order);

                            foreach (var m in menusToShift)
                            {
                                m.Order++;
                                _menuService.TUpdate(m);
                            }

                            existingMenu.Order = newOrder;
                        }
                    }
                    else
                    {
                        // Alt menüden ana menüye taşındıysa en sona at
                        int maxOrder = _menuService.GetListAll().Max(m => m.Order);
                        existingMenu.Order = maxOrder + 1;
                    }
                }

                _menuService.TUpdate(existingMenu);

                TempData["SuccessMessage"] = "Menü başarıyla güncellendi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Menü bulunamadı.";
            }

            return RedirectToAction("Index");
        }




        [HttpPost]
        public IActionResult UpdateMenuOrder(List<int> orderedIds)
        {
            if (orderedIds != null && orderedIds.Count > 0)
            {


                for (int i = 0; i < orderedIds.Count; i++)
                {
                    var menuItem = _menuService.GetByID(orderedIds[i]);
                    if (menuItem != null)
                    {
                        menuItem.Order = i;
                        _menuService.TUpdate(menuItem);
                        
                    }
                }

                return Json(new { success = true, message = "İşlem başarıyla tamamlandı." });
            }
            return Json(new { success = false, message = "Bilinmeyen bir hata oluştu" });
        }



        [HttpGet]
        public IActionResult Create()
        {

            var menus = _menuService.GetListAll();

            ViewBag.Menus = new SelectList(menus, "MenuID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Menu menu)
        {
            var result = _validator.Validate(menu);

            if (!result.IsValid)
            {
                var menus = _menuService.GetListAll();
                ViewBag.Menus = new SelectList(menus, "MenuID", "Name");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(menu);
            }

            // Order işlemi
            if (!string.IsNullOrEmpty(menu.SubMenuSelect)) // bağlı menü varsa
            {
                var parentMenu = _menuService.GetByID(Convert.ToInt32(menu.SubMenuSelect));
                if (parentMenu != null)
                {
                    int nextOrder = parentMenu.Order + 1;

                    // çakışma olmaması için mevcut Order değerlerini kaydır
                    var menusToShift = _menuService.GetListAll()
                        .Where(m => m.Order >= nextOrder)
                        .OrderByDescending(m => m.Order); // büyükten küçüğe kaydır

                    foreach (var m in menusToShift)
                    {
                        m.Order++;
                        _menuService.TUpdate(m);
                    }

                    menu.Order = nextOrder;
                }
            }
            else
            {
                // En sona ekle
                var menuList = _menuService.GetListAll();
                var maxOrder = menuList.Any() ? menuList.Max(m => m.Order) : 0;
                menu.Order = maxOrder + 1;
            }

            _menuService.TAdd(menu);

            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {


            var menu = _menuService.GetByID(id);
            if (menu != null)
            {
                try
                {
                    _menuService.TDelete(menu);
                    TempData["SuccessMessage"] = "Menü başarıyla silindi!";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Silme işlemi sırasında bir hata oluştu: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Silmek istediğiniz menü bulunamadı!";
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeMenuStatus(int id)
        {

            var menu = _menuService.GetByID(id);

            if (menu != null)
            {
                menu.MenuStatus = !menu.MenuStatus; // True ise False yap, False ise True yap
                _menuService.TUpdate(menu);
                return Json(new { success = true, newStatus = menu.MenuStatus });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeMenuHide(int id)
        {

            var menu = _menuService.GetByID(id);

            if (menu != null)
            {
                menu.MenuHide = !menu.MenuHide; // True ise False yap, False ise True yap
                _menuService.TUpdate(menu);
                return Json(new { success = true, newStatus = menu.MenuHide });
            }

            return Json(new { success = false });
        }
    }
}
