using System.Collections.Generic;
using TodoWebAPI.Core.Interfaces;
using TodoWebAPI.Core.Models;
using TodoWebAPI.ServicesInterfaces;

namespace TodoWebAPI.Service
{
    public class TodoService : ITodoService
    {
        ITodoItemRepo _todoItemRepo;

        public TodoService(ITodoItemRepo todoItemRepo) => (_todoItemRepo) = (todoItemRepo);

        public void Add(TodoItem item)
        {
            if (_todoItemRepo.Get(item.ID) == null)
                throw new KeyNotFoundException();
            _todoItemRepo.Save(item);
        }

        public void Delete(long ID)
        {
            if (_todoItemRepo.Get(ID) == null)
                throw new KeyNotFoundException();
            _todoItemRepo.Delete(ID);
        }

        public TodoItem Get(long ID)
        {
            return _todoItemRepo.Get(ID);
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todoItemRepo.GetAll();
        }
    }
}
