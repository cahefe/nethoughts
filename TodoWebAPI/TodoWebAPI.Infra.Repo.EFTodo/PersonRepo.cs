using System;
using System.Collections.Generic;
using System.Linq;
using TodoWebAPI.Core.Interfaces;
using TodoWebAPI.Core.Models;

namespace TodoWebAPI.Infra.Repo.EFTodo
{
    public class PersonRepo : IPersonRepo
    {
        readonly TodoWebAPIContext _context;

        public PersonRepo(TodoWebAPIContext context) => _context = context ?? throw new ArgumentNullException("TodoWebAPIContext");
        void IPersonRepo.Delete(long ID)
        {
            Person _person = _context.Person.FirstOrDefault(item => item.ID == ID);
            if (_person == null)
                return;
            _context.Person.Remove(_person);
            _context.SaveChanges();
        }

        Person IPersonRepo.Get(long ID)
        {
            return _context.Person.FirstOrDefault(item => item.ID == ID);
        }

        IEnumerable<Person> IPersonRepo.GetAll() => _context.Person;
        void IPersonRepo.Save(Person person)
        {
            _context.Person.Add(person);
            _context.SaveChanges();
        }
    }
}
