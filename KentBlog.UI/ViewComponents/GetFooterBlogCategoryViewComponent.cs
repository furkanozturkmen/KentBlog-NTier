using KentBlog.Business.Abstract;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class GetFooterBlogCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IBlogService _blogService;
        private readonly IAdminSettingsService _adminSettingsService;

        public GetFooterBlogCategoryViewComponent(ICategoryService categoryService, IBlogService blogService, IAdminSettingsService adminSettingsService)
        {
            _categoryService = categoryService;
            _blogService = blogService;
            _adminSettingsService = adminSettingsService;
        }
        public IViewComponentResult Invoke()
        {

            var categories = _categoryService.GetListAll();
            var blogs = _blogService.GetListAll();
            var adminsettings = _adminSettingsService.GetListAll().FirstOrDefault();

            var model = new FooterBlogCategoryViewModel
            {
                Category = categories,
                Blogs = blogs,
                AdminSettings = adminsettings
            };

            return View(model);
        }
    }
}
