using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApp.Models;
using ToDoWebApp.Models.Repository;
using ToDoWebApp.Models.ViewModels;

namespace ToDoWebApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly IToDoRepository _repository;
        private readonly ToDoDbContext _context;
        public const int PageSize = 5;

        public TaskController(IToDoRepository repository, ToDoDbContext context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index(int listId, bool completed = true, int page = 1)
            => View(new ToDoTaskViewModel
            {
                Tasks = _repository.Tasks
                .Where(x => x.ListId == listId)
                .Where(x => x.IsCompleted != completed || !completed)
                .OrderBy(task => task.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = completed ? _repository.Tasks.Where(x => x.ListId == listId)
                    .Where(x => !x.IsCompleted).Count() :
                    _repository.Tasks.Where(x => x.ListId == listId).Count()
                },
                ListId = listId,
                Completed = completed
            });

        public IActionResult Today(int page = 1) => View(new ToDoTaskViewModel
        {
            Tasks = _repository.Tasks
            .Where(x => x.DueDate == DateTime.Today)
            .Where(x => !x.IsCompleted)
            .OrderBy(task => task.Id)
            .Skip((page - 1) * PageSize)
            .Take(PageSize),

            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = _repository.Tasks.Where(x => !x.IsCompleted).Where(x => x.DueDate == DateTime.Today).Count()
            }
        });

        public ActionResult CreateTask(int listId)
        {
            return View(new ToDoTask { ListId = listId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask([Bind("ListId,Name,Description,IsCompleted," +
            "IsInProgress,DueDate,CreationData,AdditionalInfo")] ToDoTask task)
        {
            if (ModelState.IsValid)
            {
                task.CreationDate = DateTime.Now;
                if (task.IsCompleted)
                {
                    task.IsInProgress = false;
                }
                var list = await _context.Lists
                    .FirstOrDefaultAsync(x => x.Id == task.ListId);
                _context.Add(task);
                await _context.SaveChangesAsync();
                ListStatusUpdate(list);
                return RedirectToAction(nameof(Index), new { listId = task.ListId, page = 1 });
            }
            return View(task);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = _repository.Tasks
                .FirstOrDefault(x => x.Id == id);

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskToUpdate = await _context.Tasks
                .FirstOrDefaultAsync(x => x.Id == id);
            var list = await _context.Lists
                .FirstOrDefaultAsync(x => x.Id == taskToUpdate.ListId);

            if (await TryUpdateModelAsync<ToDoTask>(taskToUpdate, "",
                x => x.Name, x => x.Description, x => x.IsCompleted,
                x => x.IsInProgress, x => x.DueDate, x => x.AdditionalInfo))
            {
#pragma warning disable CS0168 // Variable is declared but never used
                try
                {
                    if (taskToUpdate.IsCompleted)
                    {
                        taskToUpdate.IsInProgress = false;
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + " Try again");
                }
#pragma warning restore CS0168 // Variable is declared but never used
                ListStatusUpdate(list);
                return RedirectToAction(nameof(Index), new { listId = taskToUpdate.ListId, page = 1 });
            }
            return View(taskToUpdate);
        }

#pragma warning disable S4144 // Methods should not have identical implementations
        public async Task<IActionResult> Delete(int? id)
#pragma warning restore S4144 // Methods should not have identical implementations
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks
                .SingleAsync(x => x.Id == id);
            _context.Tasks.Remove(task);
            var list = await _context.Lists
                .SingleAsync(x => x.Id == task.ListId);
            await _context.SaveChangesAsync();

            ListStatusUpdate(list);

            return RedirectToAction(nameof(Index), new { listId = task.ListId, page = 1 });
        }
        private bool IsListFinished(int? id)
        {
            var tasks = _context.Tasks
                .Where(x => x.ListId == id);

            var isFinished = tasks.All(x => x.IsCompleted);

            return isFinished;
        }

        private void ListStatusUpdate(ToDoList list)
        {
            if (IsListFinished(list.Id))
            {
                list.IsCompleted = true;
            }
            else
            {
                if (list.IsCompleted)
                {
                    list.IsCompleted = false;
                }
            }
            _context.SaveChanges();
        }
    }
}
