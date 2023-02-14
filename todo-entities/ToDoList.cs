using System;

namespace todo_entities
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsHidden { get; set; }
        public bool IsImportant { get; set; }
    }
}
