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
    public class SliderManager : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderManager(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public Slider GetByID(int id)
        {
            return _sliderRepository.GetByID(id);
        }

        public List<Slider> GetListAll()
        {
            return _sliderRepository.GetListAll();
        }

        public void TAdd(Slider t)
        {
            _sliderRepository.Add(t);
        }

        public void TDelete(Slider t)
        {
            _sliderRepository.Delete(t);
        }

        public void TUpdate(Slider t)
        {
            _sliderRepository.Update(t);
        }
    }
}
