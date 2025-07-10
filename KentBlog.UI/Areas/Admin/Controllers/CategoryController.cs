using FluentValidation;
using KentBlog.Business.Abstract;
using KentBlog.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace KentBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IValidator<Category> _validator;

        public CategoryController(ICategoryService categoryService, IValidator<Category> validator)
        {
            _categoryService = categoryService;
            _validator = validator;
        }

        public IActionResult Index()
        {

            var values = _categoryService.GetListAll().OrderBy(m => m.Order).ToList();
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(int id)
        {

            var item = _categoryService.GetByID(id);

            if (item != null)
            {
                item.CategoryStatus = !item.CategoryStatus; // True ise False yap, False ise True yap
                _categoryService.TUpdate(item);
                return Json(new { success = true, newStatus = item.CategoryStatus });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult UpdateOrder(List<int> orderedIds)
        {
            if (orderedIds != null && orderedIds.Count > 0)
            {


                for (int i = 0; i < orderedIds.Count; i++)
                {
                    var MainItem = _categoryService.GetByID(orderedIds[i]);
                    if (MainItem != null)
                    {
                        MainItem.Order = i;
                        _categoryService.TUpdate(MainItem);
                    }
                }

                return Json(new { success = true, message = "İşlem başarıyla tamamlandı." });
            }
            return Json(new { success = false, message = "Bilinmeyen bir hata oluştu" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeMainPage(int id)
        {

            var menu = _categoryService.GetByID(id);

            if (menu != null)
            {
                menu.MainPageStatus = !menu.MainPageStatus; // True ise False yap, False ise True yap
                _categoryService.TUpdate(menu);
                return Json(new { success = true, newStatus = menu.MainPageStatus });
            }

            return Json(new { success = false });
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category item)
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
                if (item.CategoryFile != null && item.CategoryFile.Length > 0)
                {
                    var fileExtension = Path.GetExtension(item.CategoryFile.FileName).ToLower();
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                    if (allowedExtensions.Contains(fileExtension))
                    {
                        // Dosya adını güvenli ve benzersiz hale getir
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.CategoryFile.FileName);
                        fileNameWithoutExtension = fileNameWithoutExtension.Replace(" ", "-").ToLower();
                        var uniqueName = fileNameWithoutExtension + "-" + Guid.NewGuid().ToString().Substring(0, 8) + fileExtension;

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "category", uniqueName);

                        // Aynı isimde varsa sil
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            item.CategoryFile.CopyTo(stream);
                        }

                        item.CategoryImage = "/images/category/" + uniqueName;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Geçersiz dosya formatı. Lütfen .jpg, .jpeg, .png veya .gif dosyası yükleyin.";
                        return RedirectToAction("Index");
                    }
                }

                _categoryService.TAdd(item);
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

            var item = _categoryService.GetByID(id);

            try
            {
                _categoryService.TDelete(item);
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

            var item = _categoryService.GetByID(id);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category pageitem)
        {

            var result = _validator.Validate(pageitem);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(pageitem);
            }


            try
            {
                var existingItem = _categoryService.GetByID(pageitem.CategoryID);

                if (existingItem != null)
                {
                    if (pageitem.CategoryFile != null && pageitem.CategoryFile.Length > 0)
                    {
                        var fileExtension = Path.GetExtension(pageitem.CategoryFile.FileName).ToLower();
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                        if (allowedExtensions.Contains(fileExtension))
                        {
                            // Dosya adını güvenli ve benzersiz hale getir
                            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pageitem.CategoryFile.FileName);
                            fileNameWithoutExtension = fileNameWithoutExtension.Replace(" ", "-").ToLower();
                            var uniqueName = fileNameWithoutExtension + "-" + Guid.NewGuid().ToString().Substring(0, 8) + fileExtension;

                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "category", uniqueName);

                            // Eğer daha önce bir resim kaydedilmişse eski dosyayı silebilirsin (isteğe bağlı)
                            if (!string.IsNullOrEmpty(existingItem.CategoryImage))
                            {
                                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingItem.CategoryImage.TrimStart('/'));
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                pageitem.CategoryFile.CopyTo(stream);
                            }

                            // Yeni resmi kaydet
                            existingItem.CategoryImage = "/images/category/" + uniqueName;
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Geçersiz dosya formatı. Lütfen .jpg, .jpeg, .png veya .gif dosyası yükleyin.";
                            return RedirectToAction("Index");
                        }
                    }

                    // Diğer alanların güncellenmesi
                    existingItem.CategoryName = pageitem.CategoryName;
                    existingItem.CategoryDescription = pageitem.CategoryDescription;
                    existingItem.SeoTitle = pageitem.SeoTitle;
                    existingItem.MetaDescription = pageitem.MetaDescription;
                    existingItem.MetaKeyword = pageitem.MetaKeyword;
                    existingItem.CategoryStatus = pageitem.CategoryStatus;
                    existingItem.MainPageStatus = pageitem.MainPageStatus;

                    _categoryService.TUpdate(existingItem);

                    TempData["SuccessMessage"] = "İşlem Başarıyla Tamamlandı";
                }
                else
                {
                    TempData["ErrorMessage"] = "Öğe bulunamadı";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

    }
}
