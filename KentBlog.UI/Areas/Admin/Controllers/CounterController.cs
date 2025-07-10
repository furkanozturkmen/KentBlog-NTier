using FluentValidation;
using KentBlog.Business.Abstract;
using KentBlog.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KentBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CounterController : Controller
    {
        private readonly ICounterService _counterService;
        private readonly IValidator<Counter> _validator;

        public CounterController(ICounterService counterService, IValidator<Counter> validator)
        {
            _counterService = counterService;
            _validator = validator;
        }
        public IActionResult Index()
        {
            var values = _counterService.GetListAll().OrderBy(m => m.Order).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            var menu = _counterService.GetByID(id);
            if (menu == null)
            {
                TempData["ErrorMessage"] = "Sayaç bulunamadı.";
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Counter counter)
        {

            var result = _validator.Validate(counter);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(counter);
            }


            var existingCounter = _counterService.GetByID(counter.Id);

            if (existingCounter != null)
            {

                existingCounter.Title = counter.Title;
                existingCounter.Symbol = counter.Symbol;
                existingCounter.Number = counter.Number;
                existingCounter.Status = counter.Status;

                _counterService.TUpdate(existingCounter);

                TempData["SuccessMessage"] = "Sayaç başarıyla güncellendi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Sayaç bulunamadı.";
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Create()
        {

            var menus = _counterService.GetListAll();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Counter counter)
        {

            var result = _validator.Validate(counter);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(counter);
            }


            try
            {
                _counterService.TAdd(counter);
                TempData["SuccessMessage"] = "Sayaç Başarıyla Eklendi";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bilinmeyen bir hata oluştu: " + ex.Message;
            }


            return RedirectToAction("Index");

        }


        public IActionResult Delete(int id)
        {

            var item = _counterService.GetByID(id);

            try
            {
                _counterService.TDelete(item);
                TempData["SuccessMessage"] = "İşlem başarıyla tamamlandı";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bilinmeyen bir hata oluştu: " + ex.Message;
            }


            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult UpdateOrder(List<int> orderedIds)
        {
            if (orderedIds != null && orderedIds.Count > 0)
            {


                for (int i = 0; i < orderedIds.Count; i++)
                {
                    var MainItem = _counterService.GetByID(orderedIds[i]);
                    if (MainItem != null)
                    {
                        MainItem.Order = i;
                        _counterService.TUpdate(MainItem);
                    }
                }

                return Json(new { success = true, message = "İşlem başarıyla tamamlandı." });
            }
            return Json(new { success = false, message = "Bilinmeyen bir hata oluştu" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(int id)
        {

            var item = _counterService.GetByID(id);

            if (item != null)
            {
                item.Status = !item.Status; // True ise False yap, False ise True yap
                _counterService.TUpdate(item);
                return Json(new { success = true, newStatus = item.Status });
            }

            return Json(new { success = false });
        }



    }
}
