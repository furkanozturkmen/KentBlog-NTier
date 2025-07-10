using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

namespace KentBlog.UI.ViewComponents
{
    public class BreadcrumbViewComponent : ViewComponent
    {
        private readonly Dictionary<string, string> _breadcrumbTitles = new Dictionary<string, string>
        {
            { "admin", "Anasayfa" },
            { "kurumsal", "Kurumsal" },
            { "adminsettings", "Yönetici Bilgileri" },
            { "generalsettings", "Genel Bilgiler" },
            { "contactmessages", "İletişim Mesajları" },
            { "keywords", "Anahtar Kelime" },
            { "menu", "Menü" },
            { "openingpage", "Açılış Sayfası" },
            { "slider", "Slider" },
            { "themesettings", "Tema Ayarları" },
            { "users", "Kullanıcılar" },
            { "visitors", "Ziyaretçi İstatisikleri" },
            { "blog", "Haber" },
            { "sayfa", "Anasayfa" }
        };

        public IViewComponentResult Invoke()
        {
            var pathSegments = HttpContext.Request.Path.Value.Trim('/').Split("/", StringSplitOptions.RemoveEmptyEntries).ToList();
            var breadcrumbs = new List<(string Name, string Url)>();

            string accumulatedPath = "/";

            breadcrumbs.Add(("", "/")); // İlk breadcrumb her zaman Anasayfa olsun

            for (int i = 0; i < pathSegments.Count; i++)
            {
                accumulatedPath += pathSegments[i] + "/";
                string displayName = _breadcrumbTitles.ContainsKey(pathSegments[i].ToLower())
                    ? _breadcrumbTitles[pathSegments[i].ToLower()]
                    : pathSegments[i]; // Eğer eşleşme yoksa URL parçasını göster

                breadcrumbs.Add((displayName, accumulatedPath));
            }

            return View(breadcrumbs);
        }
    }
}
