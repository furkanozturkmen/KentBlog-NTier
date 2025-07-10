using KentBlog.Data.Abstract;
using KentBlog.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Data.Concrete.EfCore
{
    public class EfContactRepository : EfGenericRepository<Contact>, IContactRepository
    {
        public EfContactRepository(Context context) : base(context)
        {
        }
    }
}
