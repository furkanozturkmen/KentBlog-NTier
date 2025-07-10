using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ViewModels
{
    public class GetCategoryByBlogViewModel
    {
        public Category? Category { get; set; }
        public List<Blog>? Blogs { get; set; }
        public int CurrentCategoryId { get; set; }
    }
}
