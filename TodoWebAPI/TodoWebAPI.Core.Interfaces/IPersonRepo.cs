using System.Collections.Generic;
using TodoWebAPI.Core.Models;

namespace TodoWebAPI.Core.Interfaces
{
    public interface IPersonRepo
    {
        IEnumerable<Person> GetAll();
        Person Get(long ID);
        void Save(Person todoItem);
        void Delete(long ID);
    }
}
