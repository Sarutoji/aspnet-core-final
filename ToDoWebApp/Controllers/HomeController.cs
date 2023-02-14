using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApp.Models;
using ToDoWebApp.Models.Repository;
using ToDoWebApp.Models.ViewModels;

namespace ToDoWebApp.Controllers
{
    public class HomeController : Controller
    {
#pragma warning disable S4487 // Unread "private" fields should be removed
        private readonly ILogger<HomeController> _logger;
#pragma warning restore S4487 // Unread "private" fields should be removed
        private readonly IToDoRepository _repository;
        private readonly ToDoDbContext _context;
        public const int PageSize = 2;

        public HomeController(ILogger<HomeController> logger, IToDoRepository repository, ToDoDbContext context)
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ViewResult Index(bool important, bool completed = true, int page = 1)
#pragma warning disable S3358 // Ternary operators should not be nested
            => View(new ToDoListViewModel
            {
                Lists = _repository.Lists
                    .Where(l => !l.IsHidden)
                    .Where(l => l.IsImportant == important || !important)
                    .Where(l => l.IsCompleted != completed || !completed)
                    .OrderBy(list => list.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = !important ?
                        (completed ? _repository.Lists.Where(l => !l.IsHidden).Where(x => !x.IsCompleted).Count() :
                            _repository.Lists.Where(l => !l.IsHidden).Count()) :
                        (completed ? _repository.Lists.Where(l => !l.IsHidden).Where(x => !x.IsCompleted).Where(l => l.IsImportant).Count() :
                        _repository.Lists.Where(l => !l.IsHidden).Where(l => l.IsImportant).Count()),
                },
                Important = important,
                Completed = completed
            });
#pragma warning restore S3358 // Ternary operators should not be nested

        public IActionResult Hidden(bool hidden, int page = 1) =>
            View(new ToDoListViewModel
            {
                Lists = _repository.Lists
                .Where(l => l.IsHidden)
                .OrderBy(l => l.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Lists.Where(l => l.IsHidden).Count()
                },
                Hidden = hidden
            });

        public ActionResult CreateList()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateList([Bind("Name,IsCompleted,IsHidden,IsImportant")] ToDoList list)
        {
            if (ModelState.IsValid)
            {
                _context.Add(list);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(list);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var list = await _context.Lists
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listToUpdate = await _context.Lists
                .FirstOrDefaultAsync(x => x.Id == id);

            if (await TryUpdateModelAsync<ToDoList>(listToUpdate, "",
                x => x.Name, x => x.IsCompleted, x => x.IsHidden, x => x.IsImportant))
            {
#pragma warning disable CS0168 // Variable is declared but never used
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save chnages. " + "Try again");
                }
#pragma warning restore CS0168 // Variable is declared but never used
                return RedirectToAction(nameof(Index));
            }
            return View(listToUpdate);
        }

        public async Task<IActionResult> Copy(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listToCopy = await _context.Lists
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (listToCopy == null)
            {
                return NotFound();
            }

            return View(listToCopy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Copy([Bind("Name,IsCompleted,IsHidden,IsImportant")] ToDoList list)
        {
            if (ModelState.IsValid)
            {
                _context.Lists.Add(list);
                await _context.SaveChangesAsync();

                var listWithTasksToCopy = await _context.Lists
                    .FirstOrDefaultAsync(x => x.Name == list.Name);
                var tasksToCopy = await FindAssignedTasksAsync(listWithTasksToCopy.Id);

                if (tasksToCopy != null)
                {
                    foreach (var task in tasksToCopy)
                    {
                        task.Id = 0;
                        task.ListId = list.Id;
                        task.CreationDate = DateTime.Now;
                        _context.Tasks.Add(task);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(list);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var list = await _context.Lists
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            var tasks = await FindAssignedTasksAsync(id);
            ViewBag.AssignedTasksCount = tasks.Count;

            if (list == null)
            {
                return NotFound();
            }

            return View(list);
        }

        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ToDoList list = await _context.Lists
                .SingleAsync(x => x.Id == id);
            var tasks = await FindAssignedTasksAsync(id);
            foreach (var task in tasks)
            {
                _context.Tasks.Remove(task);
            }
            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<ToDoTask>> FindAssignedTasksAsync(int? id)
        {
            var tasks = await _context.Tasks
                .Where(x => x.ListId == id)
                .ToListAsync();

            return tasks;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
