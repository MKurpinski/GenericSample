using System.Collections.Generic;
using System.Linq;
using GenericsUsageExample.Models;
using GenericsUsageExample.Repositories;
using GenericsUsageExample.Services;
using Moq;
using NUnit.Framework;

namespace GenericsUsageExample.Tests
{
    [TestFixture]
    public class PeopleServiceTests
    {
        private IPeopleService _peopleService;
        private Mock<IFakePeopleRepository> _peopleRepository;

        [SetUp]
        public void SetUpTests()
        {
            _peopleRepository = new Mock<IFakePeopleRepository>();
        }

        [Test]
        public void GetPerson_ShouldReturnUnsuccededResult_WhenPersonDoesNotExist()
        {
            _peopleRepository.Setup(x => x.GetPerson(It.IsAny<int>())).Returns(default(Person));
            _peopleService = new PeopleService(_peopleRepository.Object);

            var result = _peopleService.GetPerson(1);

            Assert.IsFalse(result.Succedeed);
            Assert.AreEqual(result.Errors.Count, 1);
        }
        
        [Test]
        public void GetPerson_ShouldReturnSuccededResult_WhenPersonExists()
        {
            var expectedValue = new Person
            {
                Email = "mkurpinski@gmail.com",
                FirstName = "Michał",
                LastName = "Kurpiński",
                Id = 1
            };
            _peopleRepository.Setup(x => x.GetPerson(expectedValue.Id)).Returns(expectedValue);
            _peopleService = new PeopleService(_peopleRepository.Object);

            var result = _peopleService.GetPerson(expectedValue.Id);

            Assert.IsTrue(result.Succedeed);
            AssertForSinglePerson(expectedValue, result.Value);
        }

        [Test]
        public void GetPeopleWithEmailOnDomain_ShouldReturnSuccededResult_WhenPeopleFound()
        {
            var searchingDomain = "gmail.com";
            var expectedValue = new List<Person>
            {
                new Person {Email = "mkurpinski@gmail.com", FirstName = "Michał", LastName = "Kurpiński", Id = 1},
                new Person {Email = "janKowalski@gmail.com", FirstName = "Jan", LastName = "Kowalski", Id = 2}
            };
            _peopleRepository.Setup(x => x.GetPeopleWithEmailOnDomain(searchingDomain)).Returns(expectedValue);
            _peopleService = new PeopleService(_peopleRepository.Object);

            var result = _peopleService.GetPeopleWithEmailOnDomain(searchingDomain);

            Assert.IsTrue(result.Succedeed);
            for (var i =0; i < result.Value.Count; i++)
            {
                AssertForSinglePerson(expectedValue[i], result.Value.ElementAt(i));
            }
            Assert.AreEqual(expectedValue.Count, result.Value.Count);
        }

        [Test]
        public void GetPeopleWithEmailOnDomain_ShouldReturnUnuccededResult_WhenPeopleNotFound()
        {
            var searchingDomain = "gmail.com";
            _peopleRepository.Setup(x => x.GetPeopleWithEmailOnDomain(searchingDomain)).Returns(new List<Person>());
            _peopleService = new PeopleService(_peopleRepository.Object);

            var result = _peopleService.GetPeopleWithEmailOnDomain(searchingDomain);

            Assert.IsFalse(result.Succedeed);
            Assert.IsNull(result.Value);
            Assert.AreEqual(1, result.Errors.Count);
        }

        [Test]
        public void GetPeopleWithEmailOnDomain_ShouldReturnUnuccededResult_WhenSearchingSecretDomain()
        {
            var searchingDomain = "pgs-soft.com";
            _peopleService = new PeopleService(_peopleRepository.Object);

            var result = _peopleService.GetPeopleWithEmailOnDomain(searchingDomain);

            Assert.IsFalse(result.Succedeed);
            Assert.IsNull(result.Value);
            Assert.AreEqual(1, result.Errors.Count);
        }

        private static void AssertForSinglePerson(Person expectedValue, Person result)
        {
            Assert.AreEqual(expectedValue.Email, result.Email);
            Assert.AreEqual(expectedValue.FirstName, result.FirstName);
            Assert.AreEqual(expectedValue.LastName, result.LastName);
            Assert.AreEqual(expectedValue.Id, result.Id);
        }
    }
}
