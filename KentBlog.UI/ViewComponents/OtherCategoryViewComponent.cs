using KentBlog.Business.Abstract;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class OtherCategoryViewComponent : ViewComponent
    {
        private readonly IAdminSettingsService _adminSettingsService;
        private readonly ICategoryService _categoryService;
        private readonly IBlogService _blogService;

        public OtherCategoryViewComponent(IAdminSettingsService adminSettingsService, ICategoryService categoryService, IBlogService blogService)
        {
            _adminSettingsService = adminSettingsService;
            _categoryService = categoryService;
            _blogService = blogService;
        }
        public IViewComponentResult Invoke(int currentCategoryId)
        {
            var admin = _adminSettingsService.GetListAll().FirstOrDefault();
            var allCategories = _categoryService.GetListAll();
            var allBlogs = _blogService.GetListAll();

            var categoriesWithCount = allCategories
                .Where(c => c.CategoryID != currentCategoryId)
                .Select(c => new CategoryWithBlogCountViewModel
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    BlogCount = allBlogs.Count(b => b.CategoryId == c.CategoryID)
                }).ToList();

            var viewmodel = new AdminCategoryViewModel
            {
                AdminSettings = admin,
                Categories = categoriesWithCount,
                Blogs = allBlogs
            };

            return View(viewmodel);
        }

    }
}
