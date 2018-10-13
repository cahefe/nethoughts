using System.Collections.Generic;
using TodoWebAPI.Core.Models;

namespace TodoWebAPI.Core.Interfaces
{
    public interface ITodoItemRepo
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem Get(long ID);
        void Save(TodoItem todoItem);
        void Delete(long ID);
    }
}
