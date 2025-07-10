using FluentValidation;
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
    public class AdminSettingsController : Controller
    {

        private readonly IAdminSettingsService _adminSettingsService;
        private readonly IValidator<AdminSettings> _adminSettingsValidator;

        public AdminSettingsController(IAdminSettingsService adminSettingsService, IValidator<AdminSettings> validator)
        {
            _adminSettingsService = adminSettingsService;
            _adminSettingsValidator = validator;
        }


        [HttpGet]
        public IActionResult Index()
        {

            var values = _adminSettingsService.GetListAll().FirstOrDefault();
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AdminSettings model)

        {
            var result = _adminSettingsValidator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(model);
            }


            if (model.FaviconFile != null && model.FaviconFile.Length > 0)
            {
                var fileExtension = Path.GetExtension(model.FaviconFile.FileName).ToLower();
                var allowedExtensions = new[] { ".png" };

                if (allowedExtensions.Contains(fileExtension))
                {
                    var fileName = "favicon" + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "favicon", fileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.FaviconFile.CopyTo(stream);
                    }

                    var existingSettings = _adminSettingsService.GetListAll().FirstOrDefault();

                    if (existingSettings != null)
                    {
                        existingSettings.Favicon = "/images/favicon/" + fileName;
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Geçersiz dosya formatı. Lütfen .png dosyası yükleyin.";
                    return RedirectToAction("Index");
                }
            }


            

            var values = _adminSettingsService.GetListAll().FirstOrDefault();

            if (values != null)
            {
                values.MailTop = model.MailTop;
                values.PhoneTop = model.PhoneTop;
                values.PhoneEffect = model.PhoneEffect;
                values.WhatsAppEffect = model.WhatsAppEffect;
                values.Title = model.Title;
                values.MetaDescription = model.MetaDescription;
                values.MetaKeyword = model.MetaKeyword;
                values.Cookie = model.Cookie;
                values.CookieButton = model.CookieButton;
                values.FooterInfo = model.FooterInfo;
                values.SiteStatus = model.SiteStatus;
                values.BlogFooter = model.BlogFooter;
                values.CategoryFooter = model.CategoryFooter;
                values.OtherCategory = model.OtherCategory;
                values.SpotText = model.SpotText;


                _adminSettingsService.TUpdate(values);

                TempData["SuccessMessage"] = "İşlem Başarıyla Tamamlandı";
            }
            else
            {
                TempData["ErrorMessage"] = "Admin Bilgileri bulunamadı.";
            }

            return RedirectToAction("Index");
        }
    }
}
