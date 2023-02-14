using System.Collections.Generic;

namespace ToDoWebApp.Models.ViewModels
{
    public class ToDoListViewModel
    {
        public IEnumerable<ToDoList> Lists { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public bool Important { get; set; }
        public bool Completed { get; set; }
        public bool Hidden { get; set; }
    }
}
