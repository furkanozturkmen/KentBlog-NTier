using KentBlog.Data.Abstract;
using KentBlog.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Data.Concrete.EfCore
{
    public class EfMenuRepository : EfGenericRepository<Menu>, IMenuRepository
    {
        public EfMenuRepository(Context context) : base(context)
        {
        }
    }
}
