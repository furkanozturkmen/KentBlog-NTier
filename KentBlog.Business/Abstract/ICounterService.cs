using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KentBlog.Entity.Concrete;

namespace KentBlog.Business.Abstract
{
    public interface ICounterService : IGenericService<Counter>
    {
    }
}
