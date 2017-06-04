using System.Collections.Generic;
using System.Linq;
using MvcDemo.Models;
using MvcDemo.Data;

namespace MvcDemo.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly DemoContext _context;

        public ContactRepository(DemoContext context)
        {
            _context = context;
        }

        // CREATE
        public void Add(Contact item)
        {
            _context.Contacts.Add(item);
            _context.SaveChanges();
        }

        // READ
        public IEnumerable<Contact> Find()
        {
            return _context.Contacts.ToList();
        }

        // READ
        public Contact Find(long id)
        {
            return _context.Contacts.Where(c => c.Id == id).FirstOrDefault();
        }

        // UPDATE
        public void Update(Contact item)
        {
            _context.Contacts.Update(item);
            _context.SaveChanges();
        }

        // DELETE
        public void Remove(long id)
        {
            var item = _context.Contacts.Where(c => c.Id == id).FirstOrDefault();
            if (item != null)
            {
                _context.Contacts.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
