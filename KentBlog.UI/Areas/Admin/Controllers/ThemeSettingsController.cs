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
    public class ThemeSettingsController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IThemeSettingsService _themeSettingsService;

        public ThemeSettingsController(IWebHostEnvironment env, IThemeSettingsService themeSettingsService)
        {
            _env = env;
            _themeSettingsService = themeSettingsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var values = _themeSettingsService.GetListAll().FirstOrDefault();
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ThemeSettings model)
        {

            var settings = _themeSettingsService.GetListAll().FirstOrDefault();

            if (settings != null)
            {
                settings.ThemeValue = model.ThemeValue;
                settings.ThemeColor = model.ThemeColor;
                settings.TopBarDesign = model.TopBarDesign;
                settings.PrivateCSS = model.PrivateCSS;
                settings.TopImage = model.TopImage;
                settings.FooterImage = model.FooterImage;
                settings.BlogColumn = model.BlogColumn;
                settings.BlogType = model.BlogType;
                settings.CategoryType = model.CategoryType;


                _themeSettingsService.TUpdate(settings);

                UpdateCssFile(model.ThemeColor);
                TempData["SuccessMessage"] = "İşlem Başarıyla Tamamlandı!";
            }
            else
            {
                TempData["ErrorMessage"] = "Tema ayarları bulunamadı.";
            }

            return RedirectToAction("Index");

        }

        private void UpdateCssFile(string newColor)
        {
            string filePath = Path.Combine(_env.WebRootPath, "main/news", "news.css");

            if (System.IO.File.Exists(filePath))
            {
                string cssContent = System.IO.File.ReadAllText(filePath);

                // CSS değişkenini güncelle
                string updatedCss = System.Text.RegularExpressions.Regex.Replace(
                    cssContent,
                    @"(--cnvs-themecolor:\s*)#[0-9a-fA-F]{6}",
                    $"$1{newColor}"
                );

                System.IO.File.WriteAllText(filePath, updatedCss);
            }
        }
    }
}
