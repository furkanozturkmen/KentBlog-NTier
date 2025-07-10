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
    public class VisitorManager : IVisitorService
    {
        private readonly IVisitorRepository _visitorRepository;

        public VisitorManager(IVisitorRepository visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        public Visitor GetByID(int id)
        {
            return _visitorRepository.GetByID(id);
        }

        public List<Visitor> GetListAll()
        {
            return _visitorRepository.GetListAll();
        }

        public void TAdd(Visitor t)
        {
            _visitorRepository.Add(t);
        }

        public void TDelete(Visitor t)
        {
            _visitorRepository.Delete(t);
        }

        public void TUpdate(Visitor t)
        {
            _visitorRepository.Update(t);
        }
    }
}
