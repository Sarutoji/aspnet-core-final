using System.Linq;

namespace ToDoWebApp.Models.Repository
{
    public class EFToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _context;

        public EFToDoRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public IQueryable<ToDoList> Lists => _context.Lists;
        public IQueryable<ToDoTask> Tasks => _context.Tasks;
    }
}
