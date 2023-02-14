using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ToDoWebApp.Models.Repository;

namespace ToDoWebApp.Components
{
    public class NavigationMenuViewComponent :ViewComponent
    {
        private readonly IToDoRepository _repository;

        public NavigationMenuViewComponent(IToDoRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedImportant = RouteData?.Values["important"];
            ViewBag.SelectedHidden = RouteData?.Values["hidden"];
            return View(_repository.Lists);
        }
    }
}
