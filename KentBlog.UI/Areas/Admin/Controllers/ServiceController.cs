using FluentValidation;
using KentBlog.Business.Abstract;
using KentBlog.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServiceController : Controller
    {

        private readonly IServicesService _servicesService;
        private readonly IValidator<Services> _validator;

        public ServiceController(IServicesService servicesService, IValidator<Services> validator)
        {
            _servicesService = servicesService;
            _validator = validator;
        }


        public IActionResult Index()
        {
            var values = _servicesService.GetListAll().OrderBy(m => m.Order).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            var menu = _servicesService.GetByID(id);
            if (menu == null)
            {
                TempData["ErrorMessage"] = "Sayaç bulunamadı.";
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Services service)
        {

            var result = _validator.Validate(service);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(service);
            }



            var existingService = _servicesService.GetByID(service.Id);

            if (existingService != null)
            {

                existingService.Status = service.Status;
                existingService.Title = service.Title;
                existingService.Description = service.Description;
                existingService.Icon = service.Icon;


                _servicesService.TUpdate(existingService);

                TempData["SuccessMessage"] = "İşlem Başarıyla Tamamlandı.";
            }
            else
            {
                TempData["ErrorMessage"] = "İşlem Başarısız.";
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Create()
        {

            var menus = _servicesService.GetListAll();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Services service)
        {

            var result = _validator.Validate(service);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(service);
            }


            try
            {
                _servicesService.TAdd(service);
                TempData["SuccessMessage"] = "İşlem Başarıyla Tamamlandı.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bilinmeyen bir hata oluştu: " + ex.Message;
            }


            return RedirectToAction("Index");

        }


        public IActionResult Delete(int id)
        {

            var item = _servicesService.GetByID(id);

            try
            {
                _servicesService.TDelete(item);
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
                    var MainItem = _servicesService.GetByID(orderedIds[i]);
                    if (MainItem != null)
                    {
                        MainItem.Order = i;
                        _servicesService.TUpdate(MainItem);
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

            var item = _servicesService.GetByID(id);

            if (item != null)
            {
                item.Status = !item.Status; // True ise False yap, False ise True yap
                _servicesService.TUpdate(item);
                return Json(new { success = true, newStatus = item.Status });
            }

            return Json(new { success = false });
        }

    }
}
