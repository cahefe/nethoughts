using System.Collections.Generic;
using TodoWebAPI.Core.Interfaces;
using TodoWebAPI.Core.Models;
using TodoWebAPI.ServicesInterfaces;

namespace TodoWebAPI.Service
{
    public class PersonService : IPersonService
    {
        IPersonRepo PersonRepo;

        public PersonService(IPersonRepo personRepo) => (PersonRepo) = (personRepo);

        public void Add(Person item)
        {
            if (PersonRepo.Get(item.ID) == null)
                throw new KeyNotFoundException();
            PersonRepo.Save(item);
        }

        public void Delete(long ID)
        {
            if (PersonRepo.Get(ID) == null)
                throw new KeyNotFoundException();
            PersonRepo.Delete(ID);
        }

        public Person Get(long ID)
        {
            return PersonRepo.Get(ID);
        }

        public IEnumerable<Person> GetAll()
        {
            return PersonRepo.GetAll();
        }
    }
}
