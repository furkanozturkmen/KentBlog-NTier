using KentBlog.Data.Abstract;
using KentBlog.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Data.Concrete.EfCore
{
    public class EfBlogRepository : EfGenericRepository<Blog>, IBlogRepository
    {

        private readonly Context _context;

        public EfBlogRepository(Context context) : base(context)
        {
            _context = context;
        }

        public List<Blog> GetBlogsWithCategory()
        {
            return _context.Blogs.Include(x => x.Category).ToList();
        }
    }
}
