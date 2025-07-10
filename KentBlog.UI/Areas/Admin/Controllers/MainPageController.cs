using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using KentBlog.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MainPageController : Controller
    {

        private readonly IOpeningPageService _openingPageService;

        public MainPageController(IOpeningPageService openingPageService)
        {
            _openingPageService = openingPageService;
        }

        public IActionResult Index()
        {

            var values = _openingPageService.GetListAll().OrderBy(m => m.Order).ToList();
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(int id)
        {

            var item = _openingPageService.GetByID(id);

            if (item != null)
            {
                item.Status = !item.Status; // True ise False yap, False ise True yap
                _openingPageService.TUpdate(item);
                return Json(new { success = true, newStatus = item.Status });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult UpdateItemOrder(List<int> orderedIds)
        {
            if (orderedIds != null && orderedIds.Count > 0)
            {


                for (int i = 0; i < orderedIds.Count; i++)
                {
                    var MainItem = _openingPageService.GetByID(orderedIds[i]);
                    if (MainItem != null)
                    {
                        MainItem.Order = i;
                        _openingPageService.TUpdate(MainItem);
                    }
                }

                return Json(new { success = true, message = "İşlem başarıyla tamamlandı." });
            }
            return Json(new { success = false, message = "Bilinmeyen bir hata oluştu" });
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OpeningPage item)
        {


            try
            {
                _openingPageService.TAdd(item);
                TempData["SuccessMessage"] = "İşlem başarıyla tamamlandı";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bilinmeyen bir hata oluştu: " + ex.Message;
            }


            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {

            var item = _openingPageService.GetByID(id);

            try
            {
                _openingPageService.TDelete(item);
                TempData["SuccessMessage"] = "İşlem başarıyla tamamlandı";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bilinmeyen bir hata oluştu: " + ex.Message;
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            var item = _openingPageService.GetByID(id);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(OpeningPage pageitem)
        {

            var ExistingItem = _openingPageService.GetByID(pageitem.ID);

            if (ExistingItem != null)
            {

                ExistingItem.SectionTitle = pageitem.SectionTitle;
                ExistingItem.Description = pageitem.Description;
                ExistingItem.Name = pageitem.Name;
                ExistingItem.SectionColor = pageitem.SectionColor;
                ExistingItem.Status = pageitem.Status;


                _openingPageService.TUpdate(ExistingItem);

                TempData["SuccessMessage"] = "İşlem Başarıyla Tamamlandı";
            }
            else
            {
                TempData["ErrorMessage"] = "Öğe bulunamadı";
            }



            return RedirectToAction("Index");
        }
    }
}
