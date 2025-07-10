using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ViewModels
{
    public class BlogMenuViewModel
    {
        public Menu? Menu { get; set; }
        public AdminSettings? AdminSettings { get; set; }
        public List<Blog>? blogs { get; set; }
    }
}
