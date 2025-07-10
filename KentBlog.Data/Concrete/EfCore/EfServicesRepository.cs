using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KentBlog.Data.Abstract;
using KentBlog.Entity.Concrete;

namespace KentBlog.Data.Concrete.EfCore
{
    public class EfServicesRepository : EfGenericRepository<Services>, IServicesRepository
    {
        public EfServicesRepository(Context context) : base(context)
        {
        }
    }
}
