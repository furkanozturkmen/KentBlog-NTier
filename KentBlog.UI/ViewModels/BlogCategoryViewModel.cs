using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ViewModels
{
    public class BlogCategoryViewModel
    {
        public List<Category>? Categories { get; set; }
        public List<Blog>? Blogs { get; set; }
        public ThemeSettings? ThemeSettings { get; set; }

    }
}
