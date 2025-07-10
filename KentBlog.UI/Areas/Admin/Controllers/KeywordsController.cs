using FluentValidation;
using KentBlog.Business.Abstract;
using KentBlog.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class KeywordsController : Controller
    {

        private readonly IKeywordsService _keywordsService;
        private readonly IValidator<Keywords> _validator;

        public KeywordsController(IKeywordsService keywordsService, IValidator<Keywords> validator)
        {
            _keywordsService = keywordsService;
            _validator = validator;
        }

        public IActionResult Index()
        {

            var values = _keywordsService.GetListAll().OrderBy(m => m.Order).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Keywords item)
        {

            var result = _validator.Validate(item);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(item);
            }

            try
            {
                _keywordsService.TAdd(item);
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

            var item = _keywordsService.GetByID(id);

            try
            {
                _keywordsService.TDelete(item);
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

            var item = _keywordsService.GetByID(id);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Keywords keywords)
        {

            var result = _validator.Validate(keywords);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(keywords);
            }



            var ExistingItem = _keywordsService.GetByID(keywords.KeywordID);

            if (ExistingItem != null)
            {

                ExistingItem.KeywordName = keywords.KeywordName;
                ExistingItem.KeywordLink = keywords.KeywordLink;


                _keywordsService.TUpdate(ExistingItem);

                TempData["SuccessMessage"] = "İşlem Başarıyla Tamamlandı";
            }
            else
            {
                TempData["ErrorMessage"] = "Öğe bulunamadı";
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
                    var MainItem = _keywordsService.GetByID(orderedIds[i]);
                    if (MainItem != null)
                    {
                        MainItem.Order = i;
                        _keywordsService.TUpdate(MainItem);
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

            var item = _keywordsService.GetByID(id);

            if (item != null)
            {
                item.Status = !item.Status; // True ise False yap, False ise True yap
                _keywordsService.TUpdate(item);
                return Json(new { success = true, newStatus = item.Status });
            }

            return Json(new { success = false });
        }




    }
}
