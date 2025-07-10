using KentBlog.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Data.Abstract
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        List<Blog> GetBlogsWithCategory();
    }
}
