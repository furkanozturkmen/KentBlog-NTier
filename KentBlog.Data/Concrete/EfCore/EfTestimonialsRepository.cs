using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KentBlog.Data.Abstract;
using KentBlog.Entity.Concrete;

namespace KentBlog.Data.Concrete.EfCore
{
    public class EfTestimonialsRepository : EfGenericRepository<Testimonials>, ITestimonialsRepository
    {
        public EfTestimonialsRepository(Context context) : base(context)
        {
        }
    }
}
