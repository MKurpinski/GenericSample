using System;
using System.Collections.Generic;
using System.Text;
using GenericsUsageExample.Models;

namespace GenericsUsageExample.Services
{
    public interface IPeopleService
    {
        Result<Person> GetPerson(int id);
        Result<ICollection<Person>> GetPeopleWithEmailOnDomain(string domain);
    }
}
