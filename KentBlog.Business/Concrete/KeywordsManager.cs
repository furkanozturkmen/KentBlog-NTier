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
    public class KeywordsManager : IKeywordsService
    {
        private readonly IKeywordsRepository _keywordsRepository;
        
        public KeywordsManager(IKeywordsRepository keywordsRepository) 
        { 
            _keywordsRepository = keywordsRepository; 
        }

        public Keywords GetByID(int id)
        {
           return _keywordsRepository.GetByID(id);
        }

        public List<Keywords> GetListAll()
        {
            return _keywordsRepository.GetListAll();
        }

        public void TAdd(Keywords t)
        {
            _keywordsRepository.Add(t);
        }

        public void TDelete(Keywords t)
        {
            _keywordsRepository.Delete(t);
        }

        public void TUpdate(Keywords t)
        {
            _keywordsRepository.Update(t);
        }
    }
}
