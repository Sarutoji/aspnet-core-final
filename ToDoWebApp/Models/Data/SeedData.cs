using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ToDoWebApp.Models.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ToDoDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ToDoDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Lists.Any())
            {
                context.Lists.AddRange(
                    new ToDoList
                    {
                        Name = "Example ToDo list"
                    },
                    new ToDoList
                    {
                        Name = "Example ToDo list 2",
                        IsHidden = true,
                        IsImportant = true
                    },
                    new ToDoList
                    {
                        Name = "Example ToDo list 3",
                        IsCompleted = true,
                        IsHidden = true
                    },
                    new ToDoList
                    {
                        Name = "Example ToDo list 4",
                        IsCompleted = true
                    },
                    new ToDoList
                    {
                        Name = "Example ToDo list 5",
                        IsCompleted = true,
                        IsImportant = true
                    });
                context.SaveChanges();
            }

            if (!context.Tasks.Any())
            {
                context.Tasks.AddRange(
                    new ToDoTask
                    {
                        Name = "Example Task 1",
                        Description = "Your example ToDo tasks description",
                        ListId = 1,
                        IsCompleted = true,
                        IsInProgress = false,
                        DueDate = System.DateTime.Today.AddDays(10),
                        CreationDate = System.DateTime.Now
                    },
                    new ToDoTask
                    {
                        Name = "Example Task 2",
                        Description = "Your example ToDo tasks description",
                        ListId = 1,
                        IsCompleted = false,
                        IsInProgress = false,
                        DueDate = System.DateTime.Today.AddDays(5),
                        CreationDate = System.DateTime.Now
                    },
                    new ToDoTask
                    {
                        Name = "Example Task 3",
                        Description = "Your example ToDo tasks description",
                        ListId = 1,
                        IsCompleted = false,
                        IsInProgress = true,
                        DueDate = System.DateTime.Today.AddDays(10),
                        CreationDate = System.DateTime.Now
                    },
                    new ToDoTask
                    {
                        Name = "Example Task 4",
                        Description = "Your example ToDo tasks description",
                        ListId = 2,
                        IsCompleted = false,
                        IsInProgress = true,
                        DueDate = System.DateTime.Today.AddDays(10),
                        CreationDate = System.DateTime.Now
                    },
                    new ToDoTask
                    {
                        Name = "Example Task 5",
                        Description = "Your example ToDo tasks description",
                        ListId = 2,
                        IsCompleted = false,
                        IsInProgress = true,
                        DueDate = System.DateTime.Today.AddDays(10),
                        CreationDate = System.DateTime.Now,
                        AdditionalInfo = "Your Task additional Info"
                    },
                    new ToDoTask
                    {
                        Name = "Example Task 6",
                        Description = "Your example ToDo tasks description",
                        ListId = 2,
                        IsCompleted = false,
                        IsInProgress = true,
                        DueDate = System.DateTime.Today.AddDays(10),
                        CreationDate = System.DateTime.Now,
                        AdditionalInfo = "Your Task additional Info"
                    });
                context.SaveChanges();
            }
        }
    }
}
