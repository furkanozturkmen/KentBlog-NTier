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
    public class TestimonialsManager : ITestimonialsService
    {
        private readonly ITestimonialsRepository _testimonialsRepository;

        public TestimonialsManager(ITestimonialsRepository testimonialsRepository)
        {
            _testimonialsRepository = testimonialsRepository;
        }

        public Testimonials GetByID(int id)
        {
            return _testimonialsRepository.GetByID(id);
        }

        public List<Testimonials> GetListAll()
        {
            return _testimonialsRepository.GetListAll();
        }

        public void TAdd(Testimonials t)
        {
            _testimonialsRepository.Add(t);
        }

        public void TDelete(Testimonials t)
        {
            _testimonialsRepository.Delete(t);
        }

        public void TUpdate(Testimonials t)
        {
            _testimonialsRepository.Update(t);
        }
    }
}
