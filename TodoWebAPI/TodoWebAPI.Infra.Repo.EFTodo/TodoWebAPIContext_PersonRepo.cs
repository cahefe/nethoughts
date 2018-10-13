using System.Collections.Generic;
using System.Linq;
using TodoWebAPI.Core.Interfaces;
using TodoWebAPI.Core.Models;

namespace TodoWebAPI.Infra.Repo.EFTodo
{
    public partial class TodoWebAPIContext: IPersonRepo
    {
        void IPersonRepo.Delete(long ID)
        {
            Person _person = Person.FirstOrDefault(item => item.ID == ID);
            if (_person == null)
                return;
            Person.Remove(_person);
            SaveChanges();
        }

        Person IPersonRepo.Get(long ID)
        {
            return Person.FirstOrDefault(item => item.ID == ID);
        }

        IEnumerable<Person> IPersonRepo.GetAll() => Person;
        void IPersonRepo.Save(Person person)
        {
            Person.Add(person);
            SaveChanges();
        }
    }
}
