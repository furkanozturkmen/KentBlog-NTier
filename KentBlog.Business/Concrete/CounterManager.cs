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
    public class CounterManager : ICounterService
    {
        private readonly ICounterRepository _counterRepository;

        public CounterManager(ICounterRepository counterRepository)
        {
            _counterRepository = counterRepository;
        }

        public Counter GetByID(int id)
        {
            return _counterRepository.GetByID(id);
        }

        public List<Counter> GetListAll()
        {
            return _counterRepository.GetListAll();
        }

        public void TAdd(Counter t)
        {
            _counterRepository.Add(t);
        }

        public void TDelete(Counter t)
        {
            _counterRepository.Delete(t);
        }

        public void TUpdate(Counter t)
        {
            _counterRepository.Update(t);
        }
    }
}
