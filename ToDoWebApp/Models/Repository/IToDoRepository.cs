using System.Linq;

namespace ToDoWebApp.Models.Repository
{
    public interface IToDoRepository
    {
        IQueryable<ToDoList> Lists { get; }
        IQueryable<ToDoTask> Tasks { get; }
    }
}
