using KentBlog.Business.Abstract;
using KentBlog.Entity.Concrete;
using KentBlog.UI.Utilities;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Controllers
{
    public class KategoriController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBlogService _blogService;

        public KategoriController(ICategoryService categoryService, IBlogService blogService)
        {
            _categoryService = categoryService;
            _blogService = blogService;
        }

        [Route("kategori/{id:int}/{seo}")]
        public IActionResult Index(int id, string seo)
        {

            var values = _categoryService.GetByID(id);

            if (values == null || !values.CategoryStatus)
            {
                return NotFound();
            }


            var blogs = _blogService.GetListAll()
             .Where(x => x.CategoryId == id)
             .ToList();


            ViewData["IsMenuPage"] = true;
            ViewData["PageTitle"] = "Tüm Kategoriler";

            if (values == null)
                return NotFound();

            var dogruSeo = Helpers.UrlDostuYap(values.SeoTitle);
            if (seo != dogruSeo)
            {
                return RedirectToAction("Index", new { id = id, seo = dogruSeo });
            }

            var viewmodel = new GetCategoryByBlogViewModel 
            { 
              Blogs = blogs, 
              Category = values,
              CurrentCategoryId = values.CategoryID
              
            };
            
            return View(viewmodel);
        }
    }
}
