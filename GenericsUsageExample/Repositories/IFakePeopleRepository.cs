using System.Collections.Generic;
using GenericsUsageExample.Models;

namespace GenericsUsageExample.Repositories
{
    public interface IFakePeopleRepository
    {
        Person GetPerson(int id);
        ICollection<Person> GetPeopleWithEmailOnDomain(string domain);
    }
}
