using KentBlog.Business.Abstract;
using KentBlog.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Controllers
{
    public class HaberController : Controller
    {
        private readonly IBlogService _blogService;

        public HaberController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [Route("haber/{id:int}/{seo}")]
        public IActionResult Index(int id, string seo)
        {
            var values = _blogService.GetByID(id);

            if (values == null || !values.BlogStatus)
            {
                return NotFound();
            }

            ViewData["PageTitle"] = values.Title;
            ViewData["IsMenuPage"] = true;


            if (values == null)
                return NotFound();

            var dogruSeo = KentBlog.UI.Utilities.Helpers.UrlDostuYap(values.SeoTitle);
            if (seo != dogruSeo)
            {
                return RedirectToAction("Index", new { id = id, seo = dogruSeo });
            }

            return View(values);
        }
    }
}
