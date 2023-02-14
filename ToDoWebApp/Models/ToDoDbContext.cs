using Microsoft.EntityFrameworkCore;

namespace ToDoWebApp.Models
{
    public class ToDoDbContext :DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
            : base(options) { }

        public DbSet<ToDoTask> Tasks { get; set; }
        public DbSet<ToDoList> Lists { get; set; }
    }
}
