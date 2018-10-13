using System.Collections.Generic;
using TodoWebAPI.Core.Models;

namespace TodoWebAPI.ServicesInterfaces
{
    public interface ITodoService
    {
        void Add(TodoItem item);
        TodoItem Get(long ID);
        IEnumerable<TodoItem> GetAll();
        void Delete(long ID);
    }
}
