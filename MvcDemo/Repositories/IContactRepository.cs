using MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDemo.Repositories
{
    public interface IContactRepository
    {
        // CREATE
        void Add(Contact item);

        // READ
        IEnumerable<Contact> Find();
        Contact Find(long id);

        // UPDATE
        void Update(Contact item);

        // DELETE
        void Remove(long id);
    }
}
