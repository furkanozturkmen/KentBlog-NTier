using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KentBlog.Entity.Concrete;

namespace KentBlog.Data.Abstract
{
    public interface ICounterRepository : IGenericRepository<Counter>
    {
    }
}
