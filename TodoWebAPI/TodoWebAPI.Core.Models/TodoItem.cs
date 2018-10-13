using System;

namespace TodoWebAPI.Core.Models
{
    public enum TodoItemTypeEnum : byte
    {
        Undefined = 0,
        Task = 1,
        SubTask = 2
    }
    public class TodoItem
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public TodoItemTypeEnum Type { get; set; }
    }
}
