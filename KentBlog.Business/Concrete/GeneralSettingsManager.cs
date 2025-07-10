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
    public class GeneralSettingsManager : IGeneralSettingsService
    {

        private readonly IGeneralSettingsRepository _generalsettingsRepository;

        public GeneralSettingsManager(IGeneralSettingsRepository generalsettingsRepository)
        {
             _generalsettingsRepository = generalsettingsRepository;
        }

        public GeneralSettings GetByID(int id)
        {
            return _generalsettingsRepository.GetByID(id);
        }

        public List<GeneralSettings> GetListAll()
        {
            return _generalsettingsRepository.GetListAll();
        }

        public void TAdd(GeneralSettings t)
        {
            _generalsettingsRepository.Add(t);
        }

        public void TDelete(GeneralSettings t)
        {
            _generalsettingsRepository.Delete(t);
        }

        public void TUpdate(GeneralSettings t)
        {
            _generalsettingsRepository.Update(t);
        }
    }
}
