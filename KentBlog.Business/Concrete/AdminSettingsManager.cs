using KentBlog.Business.Abstract;
using KentBlog.Data.Abstract;
using KentBlog.Data.Concrete.EfCore;
using KentBlog.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Business.Concrete
{
    public class AdminSettingsManager : IAdminSettingsService
    {

        private readonly IAdminSettingsRepository _adminsettingsRepository;

        public AdminSettingsManager(IAdminSettingsRepository adminsettingsRepository)
        {
            _adminsettingsRepository = adminsettingsRepository;
        }


        public AdminSettings GetByID(int id)
        {
           return _adminsettingsRepository.GetByID(id);
        }

        public List<AdminSettings> GetListAll()
        {
           return _adminsettingsRepository.GetListAll();
        }

        public void TAdd(AdminSettings t)
        {
            _adminsettingsRepository.Add(t);
        }

        public void TDelete(AdminSettings t)
        {
            _adminsettingsRepository.Delete(t);
        }

        public void TUpdate(AdminSettings t)
        {
            _adminsettingsRepository.Update(t);
        }
    }
}
