using System.Collections.Generic;
using System.Linq;
using TodoWebAPI.Core.Interfaces;
using TodoWebAPI.Core.Models;

namespace TodoWebAPI.Infra.Repo.EFTodo
{
    public partial class TodoWebAPIContext : ITodoItemRepo
    {
        void ITodoItemRepo.Delete(long ID)
        {
            TodoItem _todoItem = TodoItem.FirstOrDefault(item => item.ID == ID);
            if (_todoItem == null)
                return;
            TodoItem.Remove(_todoItem);
            SaveChanges();
        }

        TodoItem ITodoItemRepo.Get(long ID)
        {
            return TodoItem.FirstOrDefault(item => item.ID == ID);
        }

        IEnumerable<TodoItem> ITodoItemRepo.GetAll()
        {
            return TodoItem;
        }
        void ITodoItemRepo.Save(TodoItem todoItem)
        {
            TodoItem.Add(todoItem);
            SaveChanges();
        }
    }
}
