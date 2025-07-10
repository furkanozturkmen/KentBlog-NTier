using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ViewModels
{
    public class AdminBlogViewModel
    {
        public AdminSettings AdminSettings { get; set; }
        public List<Blog>? blogs { get; set; }
        public ThemeSettings? ThemeSettings { get; set; }
    }
}
