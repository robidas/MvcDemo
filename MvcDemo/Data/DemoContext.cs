using Microsoft.EntityFrameworkCore;
using MvcDemo.Models;

namespace MvcDemo.Data
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
