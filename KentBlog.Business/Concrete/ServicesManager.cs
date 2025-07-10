using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KentBlog.Business.Abstract;
using KentBlog.Data.Abstract;
using KentBlog.Data.Concrete.EfCore;
using KentBlog.Entity.Concrete;

namespace KentBlog.Business.Concrete
{

    public class ServicesManager : IServicesService
    {

        private readonly IServicesRepository _servicesRepository;

        public ServicesManager(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        public Services GetByID(int id)
        {
            return _servicesRepository.GetByID(id);
        }

        public List<Services> GetListAll()
        {
            return _servicesRepository.GetListAll();
        }

        public void TAdd(Services t)
        {
            _servicesRepository.Add(t);
        }

        public void TDelete(Services t)
        {
            _servicesRepository.Delete(t);
        }

        public void TUpdate(Services t)
        {
            _servicesRepository.Update(t);
        }
    }
}
