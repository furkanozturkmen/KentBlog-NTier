using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ViewModels
{
    public class FooterBlogCategoryViewModel
    {
        public List<Category>? Category { get; set; }

        public List<Blog>? Blogs { get; set; }

        public AdminSettings? AdminSettings { get; set; }
    }
}
