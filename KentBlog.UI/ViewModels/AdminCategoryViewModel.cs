using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ViewModels
{
    public class AdminCategoryViewModel
    {
        public AdminSettings AdminSettings { get; set; }
        public List<CategoryWithBlogCountViewModel> Categories { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
