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
    public class ContactManager : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactManager(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Contact GetByID(int id)
        {
            return _contactRepository.GetByID(id);
        }

        public List<Contact> GetListAll()
        {
            return _contactRepository.GetListAll();
        }

        public void TAdd(Contact t)
        {
            _contactRepository.Add(t);
        }

        public void TDelete(Contact t)
        {
            _contactRepository.Delete(t);
        }

        public void TUpdate(Contact t)
        {
            _contactRepository.Update(t);
        }
    }
}
