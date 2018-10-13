using System.Collections.Generic;
using TodoWebAPI.Core.Models;

namespace TodoWebAPI.ServicesInterfaces
{
    public interface IPersonService
    {
        void Add(Person item);
        Person Get(long ID);
        IEnumerable<Person> GetAll();
        void Delete(long ID);
    }
}
