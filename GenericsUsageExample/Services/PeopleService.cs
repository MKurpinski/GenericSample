using System.Collections.Generic;
using System.Linq;
using GenericsUsageExample.Models;
using GenericsUsageExample.Repositories;

namespace GenericsUsageExample.Services
{
    public class PeopleService: IPeopleService
    {
        private readonly IFakePeopleRepository _peopleRepository;
        private const string SuperSecretDomain = "pgs-soft.com";

        public PeopleService(IFakePeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public Result<Person> GetPerson(int id)
        {
            var person = _peopleRepository.GetPerson(id);
            if (person == null)
            {
                return Result<Person>.Failure(new List<string> {$"There is no person with id: {id}"});
            }
            return Result<Person>.Success(person);
        }

        public Result<ICollection<Person>> GetPeopleWithEmailOnDomain(string domain)
        {
            if (domain.Equals(SuperSecretDomain))
            {
               return new Result<ICollection<Person>>(null, false, new List<string>{$"Cannot find people with email on domain: {domain}, It's super secret!"});
            }

            var foundPeople = _peopleRepository.GetPeopleWithEmailOnDomain(domain);

            if (!foundPeople.Any())
            {
                return new Result<ICollection<Person>>(null, false, new List<string> { $"Nobody has an email on domain: {domain}" });
            }

            return Result<ICollection<Person>>.Success(foundPeople);
        }
    }
}
