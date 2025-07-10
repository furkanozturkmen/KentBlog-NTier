using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KentBlog.Business.Abstract;
using KentBlog.Data.Abstract;
using KentBlog.Entity.Concrete;

namespace KentBlog.Business.Concrete
{
    public class AboutUsManager : IAboutUsService
    {
        private readonly IAboutUsRepository _aboutUsRepository;

        public AboutUsManager(IAboutUsRepository aboutUsRepository)
        {
         _aboutUsRepository = aboutUsRepository;   
        }

        public AboutUs GetByID(int id)
        {
            return _aboutUsRepository.GetByID(id);
        }

        public List<AboutUs> GetListAll()
        {
            return _aboutUsRepository.GetListAll();
        }

        public void TAdd(AboutUs t)
        {
            _aboutUsRepository.Add(t);
        }

        public void TDelete(AboutUs t)
        {
            _aboutUsRepository.Delete(t);
        }

        public void TUpdate(AboutUs t)
        {
            _aboutUsRepository.Update(t);
        }
    }
}
