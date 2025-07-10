using KentBlog.Business.Abstract;
using KentBlog.Data.Abstract;
using KentBlog.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Business.Concrete
{
    public class MenuManager : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuManager(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public Menu GetByID(int id)
        {
            return _menuRepository.GetByID(id);
        }

        public List<Menu> GetListAll()
        {
            return _menuRepository.GetListAll();
        }

        public void TAdd(Menu t)
        {
            _menuRepository.Add(t);
        }

        public void TDelete(Menu t)
        {
            _menuRepository.Delete(t);
        }

        public void TUpdate(Menu t)
        {
            _menuRepository.Update(t);
        }
    }
}
