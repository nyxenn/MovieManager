using MMApi.Internal.DataAccess;
using MMApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMApi.Helpers
{
    public class PersonHelper
    {
        private readonly MovieContext _context;

        public PersonHelper(MovieContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> CreatePersonList(string peopleString)
        {
            string[] peopleSplit = peopleString.Split(',');
            List<Person> people = new List<Person>();
            int newPersonCount = 0;

            foreach (var p in peopleSplit)
            {
                Person person = _context.People.FirstOrDefault(ps => ps.Name == p.Trim());

                if (person == null)
                {
                    person = new Person(p.Trim());
                    _context.People.Add(person);
                    newPersonCount++;
                }

                people.Add(person);
            }

            if (newPersonCount > 0)
            {
                await _context.SaveChangesAsync();
            }

            return people;
        }

        public async Task<List<MoviePerson>> GetMoviePersonList(string peopleString, string type)
        {
            List<MoviePerson> moviePersonList = new List<MoviePerson>();
            List<Person> people = new List<Person>();
            people = await CreatePersonList(peopleString);

            foreach (var person in people)
            {
                MoviePerson moviePerson = new MoviePerson();
                moviePerson.Type = type;
                moviePerson.Person = person;
                moviePersonList.Add(moviePerson);
            }

            return moviePersonList;
        }
    }
}
