using KentBlog.Business.Abstract;
using KentBlog.Data.Abstract;
using KentBlog.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Business.Concrete
{
    public class OpeningPageManager : IOpeningPageService
    {

        private readonly IOpeningPageRepository _openingpageRepository;

        public OpeningPageManager(IOpeningPageRepository openingpageRepository)
        {
            _openingpageRepository = openingpageRepository;
        }

        public OpeningPage GetByID(int id)
        {
            return _openingpageRepository.GetByID(id);
        }

        public List<OpeningPage> GetListAll()
        {
            return _openingpageRepository.GetListAll();
        }

        public void TAdd(OpeningPage t)
        {
            _openingpageRepository.Add(t);
        }

        public void TDelete(OpeningPage t)
        {
            _openingpageRepository.Delete(t);
        }

        public void TUpdate(OpeningPage t)
        {
            _openingpageRepository.Update(t);
        }
    }
}
