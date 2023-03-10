using System;
using System.Collections.Generic;
using System.Text;

namespace todo_entities
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsInProgress { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string AdditionalInfo { get; set; }

    }
}
