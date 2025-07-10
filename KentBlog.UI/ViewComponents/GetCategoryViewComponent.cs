using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class GetCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IBlogService _blogService;
        private readonly IThemeSettingsService _themeSettingsService;

        public GetCategoryViewComponent(ICategoryService categoryService, IBlogService blogService, IThemeSettingsService themeSettingsService)
        {
            _categoryService = categoryService;
            _blogService = blogService;
            _themeSettingsService = themeSettingsService;
        }
        public IViewComponentResult Invoke()
        {

            var categories = _categoryService.GetListAll();
            var blogs = _blogService.GetListAll();
            var theme = _themeSettingsService.GetListAll().FirstOrDefault();

            var model = new BlogCategoryViewModel
            {
                Categories = categories,
                Blogs = blogs,
                ThemeSettings = theme   
            };

            return View(model);
        }
    }

}
