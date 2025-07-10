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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        
        public Category GetByID(int id)
        {
           return _categoryRepository.GetByID(id);
        }

        public List<Category> GetListAll()
        {
            return _categoryRepository.GetListAll();
        }

        public void TAdd(Category t)
        {
            _categoryRepository.Add(t);
        }

        public void TDelete(Category t)
        {
            _categoryRepository.Delete(t);
        }

        public void TUpdate(Category t)
        {
            _categoryRepository.Update(t);
        }
    }
}
