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
    public class ThemeSettingsManager : IThemeSettingsService
    {
        private readonly IThemeSettingsRepository _themeSettingsRepository;

        public ThemeSettingsManager(IThemeSettingsRepository themeSettingsRepository)
        {
            _themeSettingsRepository = themeSettingsRepository;
        }

        public ThemeSettings GetByID(int id)
        {
            return _themeSettingsRepository.GetByID(id);
        }

        public List<ThemeSettings> GetListAll()
        {
            return _themeSettingsRepository.GetListAll();
        }

        public void TAdd(ThemeSettings t)
        {
            _themeSettingsRepository.Add(t);
        }

        public void TDelete(ThemeSettings t)
        {
            _themeSettingsRepository.Delete(t);
        }

        public void TUpdate(ThemeSettings t)
        {
            _themeSettingsRepository.Update(t);
        }
    }
}
