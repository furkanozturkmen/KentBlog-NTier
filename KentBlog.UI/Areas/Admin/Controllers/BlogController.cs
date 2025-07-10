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
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly IValidator<Blog> _validator;

        public BlogController(IBlogService blogService, ICategoryService categoryService, IValidator<Blog> validator)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _validator = validator;
        }

        public IActionResult Index()
        {

            var categories = _categoryService.GetListAll();
            ViewBag.Menus = new SelectList(categories, "CategoryID", "CategoryName");
            @ViewBag.blogcount = _blogService.GetListAll().Count;

            var values = _blogService.GetListAll().OrderBy(m => m.Order).ToList();
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(int id)
        {

            var item = _blogService.GetByID(id);

            if (item != null)
            {
                item.BlogStatus = !item.BlogStatus; // True ise False yap, False ise True yap
                _blogService.TUpdate(item);
                return Json(new { success = true, newStatus = item.BlogStatus });
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
                    var MainItem = _blogService.GetByID(orderedIds[i]);
                    if (MainItem != null)
                    {
                        MainItem.Order = i;
                        _blogService.TUpdate(MainItem);
                    }
                }

                return Json(new { success = true, message = "İşlem başarıyla tamamlandı." });
            }
            return Json(new { success = false, message = "Bilinmeyen bir hata oluştu" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSelectedBlog([FromBody] List<int> selectedIds)
        {
            if (selectedIds == null || selectedIds.Count == 0)
            {
                TempData["ErrorMessage"] = "Lütfen en az bir öğe seçin!";
                return Json(new { success = false });
            }

            try
            {

                var menusToDelete = _blogService.GetListAll().Where(m => selectedIds.Contains(m.Id)).ToList();

                if (menusToDelete.Count == 0)
                {
                    TempData["ErrorMessage"] = "Seçilen ID'lere ait kayıt bulunamadı!";
                    return Json(new { success = false });
                }

                foreach (var menu in menusToDelete)
                {
                    _blogService.TDelete(menu);
                }

                TempData["SuccessMessage"] = $"{menusToDelete.Count} öğe başarıyla silindi!";
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Silme işlemi başarısız oldu: " + ex.Message;
                return Json(new { success = false });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeHide(int id)
        {

            var menu = _blogService.GetByID(id);

            if (menu != null)
            {
                menu.BlogHide = !menu.BlogHide; // True ise False yap, False ise True yap
                _blogService.TUpdate(menu);
                return Json(new { success = true, newStatus = menu.BlogHide });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeMainPageStatus(int id)
        {

            var menu = _blogService.GetByID(id);

            if (menu != null)
            {
                menu.MainPageStatus = !menu.MainPageStatus; // True ise False yap, False ise True yap
                _blogService.TUpdate(menu);
                return Json(new { success = true, newStatus = menu.BlogHide });
            }

            return Json(new { success = false });
        }




        [HttpGet]
        public IActionResult Create()
        {

            var categories = _categoryService.GetListAll();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "CategoryName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog item)
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
                if (item.BlogFile != null && item.BlogFile.Length > 0)
                {
                    var fileExtension = Path.GetExtension(item.BlogFile.FileName).ToLower();
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                    if (allowedExtensions.Contains(fileExtension))
                    {
                        // Dosya adını güvenli ve benzersiz hale getir
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.BlogFile.FileName);
                        fileNameWithoutExtension = fileNameWithoutExtension.Replace(" ", "-").ToLower();
                        var uniqueName = fileNameWithoutExtension + "-" + Guid.NewGuid().ToString().Substring(0, 8) + fileExtension;

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "blog", uniqueName);

                        // Aynı isimde varsa sil
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            item.BlogFile.CopyTo(stream);
                        }

                        item.Image = "/images/blog/" + uniqueName;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Geçersiz dosya formatı. Lütfen .jpg, .jpeg, .png veya .gif dosyası yükleyin.";
                        return RedirectToAction("Index");
                    }
                }

                _blogService.TAdd(item);
                TempData["SuccessMessage"] = "İşlem başarıyla tamamlandı";
            }
            catch (Exception ex)
            {
                var fullError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                TempData["ErrorMessage"] = "Bilinmeyen bir hata oluştu: " + fullError;
            }

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {

            var item = _blogService.GetByID(id);

            try
            {
                _blogService.TDelete(item);
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

            var categories = _categoryService.GetListAll();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "CategoryName");

            var item = _blogService.GetByID(id);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog pageitem)
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
                var existingItem = _blogService.GetByID(pageitem.Id);

                if (existingItem != null)
                {
                    if (pageitem.BlogFile != null && pageitem.BlogFile.Length > 0)
                    {
                        var fileExtension = Path.GetExtension(pageitem.BlogFile.FileName).ToLower();
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                        if (allowedExtensions.Contains(fileExtension))
                        {
                            // Dosya adını güvenli ve benzersiz hale getir
                            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pageitem.BlogFile.FileName);
                            fileNameWithoutExtension = fileNameWithoutExtension.Replace(" ", "-").ToLower();
                            var uniqueName = fileNameWithoutExtension + "-" + Guid.NewGuid().ToString().Substring(0, 8) + fileExtension;

                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "category", uniqueName);

                            // Eğer daha önce bir resim kaydedilmişse eski dosyayı silebilirsin (isteğe bağlı)
                            if (!string.IsNullOrEmpty(existingItem.Image))
                            {
                                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingItem.Image.TrimStart('/'));
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                pageitem.BlogFile.CopyTo(stream);
                            }

                            // Yeni resmi kaydet
                            existingItem.Image = "/images/category/" + uniqueName;
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Geçersiz dosya formatı. Lütfen .jpg, .jpeg, .png veya .gif dosyası yükleyin.";
                            return RedirectToAction("Index");
                        }
                    }

                    // Diğer alanların güncellenmesi
                    existingItem.Title = pageitem.Title;
                    existingItem.Content = pageitem.Content;
                    existingItem.Date = pageitem.Date;
                    existingItem.BlogStatus = pageitem.BlogStatus;
                    existingItem.MetaKeyword = pageitem.MetaKeyword;
                    existingItem.MetaDescription = pageitem.MetaDescription;
                    existingItem.SeoTitle = pageitem.SeoTitle;
                    existingItem.CategoryId = pageitem.CategoryId;
                    existingItem.CategoryStatus = pageitem.CategoryStatus;
                    existingItem.SliderStatus = pageitem.SliderStatus;
                    existingItem.BlogHide = pageitem.BlogHide;
                    existingItem.MainPageStatus = pageitem.MainPageStatus;

                    _blogService.TUpdate(existingItem);

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
