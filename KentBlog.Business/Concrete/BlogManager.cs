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
    public class BlogManager : IBlogService
    {

        private readonly IBlogRepository _blogRepository;

        public BlogManager(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public List<Blog> GetBlogsWithCategory()
        {
            return _blogRepository.GetBlogsWithCategory();
        }

        public Blog GetByID(int id)
        {
           return _blogRepository.GetByID(id);
        }

        public List<Blog> GetListAll()
        {
            return _blogRepository.GetListAll();
        }

        public void TAdd(Blog t)
        {
            _blogRepository.Add(t);
        }

        public void TDelete(Blog t)
        {
            _blogRepository.Delete(t);
        }

        public void TUpdate(Blog t)
        {
            _blogRepository.Update(t);
        }
    }
}
