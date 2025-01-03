using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Data;
using WebAppProject.Models;
using WebAppProject.Validation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
namespace WebAppProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController :ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly IValidator<Person> _personValidator;

        public PersonController(DataContext dbContext, IValidator<Person> personValidator)
        {
            _dbContext = dbContext;
            _personValidator = personValidator;
        }

        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            _dbContext.Persons.Add(person);
            _dbContext.SaveChanges();
            return Ok(_dbContext.Persons.ToList());
        }

        [HttpGet]
        public IActionResult GetPersons()
        {
            var persons = _dbContext.Persons.Include(p => p.PersonAddress).ToList();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult GetPersonById(int id)
        {
            //var person = _dbContext.Persons.Find(id);
            var person = _dbContext.Persons.Include(p => p.PersonAddress).Where(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }



        [HttpDelete("{id}")]
        public IActionResult DeletePersonById(int id)
        {
            var person = _dbContext.Persons.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            _dbContext.Persons.Remove(person);
            _dbContext.SaveChanges();
            return Ok(_dbContext.Persons.ToList());
        }

        [HttpGet("filterWithCityAndSalary")]
        public IActionResult FilterPerson(string City, double Salary)
        {
            var person = _dbContext.Persons.Include(p => p.PersonAddress).Where(x => x.Salary >= Salary && x.PersonAddress.City == City);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePersonId(int id, Person updatedPerson)
        {
            var person = _dbContext.Persons.Include(p => p.PersonAddress).FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            person.CreateDate = updatedPerson.CreateDate;
            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.JobPosition = updatedPerson.JobPosition;
            person.Salary = updatedPerson.Salary;
            person.WorkExperience = updatedPerson.WorkExperience;
            person.PersonAddress.Country = updatedPerson.PersonAddress.Country;
            person.PersonAddress.City = updatedPerson.PersonAddress.City;
            person.PersonAddress.Homenumber = updatedPerson.PersonAddress.Homenumber;

            _dbContext.SaveChanges();

            return Ok(person);
        }



    }
}
