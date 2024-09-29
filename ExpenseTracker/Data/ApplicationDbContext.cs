using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_configuration.GetConnectionString("Database"));
        }
        public DbSet<User> Users {  get; set; } 
        public DbSet<Expense> Expenses { get; set; }
    }
}
