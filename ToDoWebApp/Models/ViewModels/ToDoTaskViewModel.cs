using System.Collections.Generic;

namespace ToDoWebApp.Models.ViewModels
{
    public class ToDoTaskViewModel
    {
        public IEnumerable<ToDoTask> Tasks { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int ListId { get; set; }
        public bool Completed { get; set; }
    }
}
