using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodoListApp
{
    public class todoContext : DbContext
    {
        public DbSet<todoItems> todoItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=TodoList;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<todoItems>().HasKey(t => t.id); // Explicitly define 'id' as the primary key
            modelBuilder.Entity<todoItems>().HasData(
                new todoItems { id = 1, description = "test1" },
                new todoItems { id = 2, description = "test2" }
            );
        }
    }
}