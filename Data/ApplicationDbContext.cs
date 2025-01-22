using Microsoft.EntityFrameworkCore;
using WowPay.Models.Entities;

namespace WowPay.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<Person> Persons { get; set; }
    }
}
