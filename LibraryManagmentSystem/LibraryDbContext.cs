using LibaryManagmentSystemAPI.Models;
using LibraryManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibaryManagmentSystemAPI
{
    public class LibraryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            options.UseSqlServer("Data Source=(local);Initial Catalog=WebApi; Integrated Security=true; TrustServerCertificate=True");
        }
        public DbSet<Book> books { get; set; }
        public DbSet<BorrowingTransaction> borrowingTransactions { get; set; }
        public DbSet<Patron> patrons { get; set; }
        public DbSet<User> users { get; set; }
        public IEnumerable<object> Patron { get; internal set; }
    }
}
