using Microsoft.EntityFrameworkCore;
using PeopleManager.Model;

namespace PeopleManager.Ui.Mvc.Core
{
    public class PeopleManagerDbContext(DbContextOptions<PeopleManagerDbContext> options) : DbContext(options)
    {
        public DbSet<Person> People => Set<Person>();

        public void Seed()
        {
            
            var people = new List<Person>
            {
                new Person { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Person { FirstName = "Jane", LastName = "Smith" }, // Email is omitted
                new Person { FirstName = "Michael", LastName = "Johnson", Email = "michael.johnson@example.com" },
                new Person { FirstName = "Emily", LastName = "Davis" }, // Email is omitted
                new Person { FirstName = "Chris", LastName = "Brown", Email = "chris.brown@example.com" },
                new Person { FirstName = "Jessica", LastName = "Wilson" }, // Email is omitted
                new Person { FirstName = "David", LastName = "Garcia", Email = "david.garcia@example.com" },
                new Person { FirstName = "Sophia", LastName = "Martinez" }, // Email is omitted
                new Person { FirstName = "Daniel", LastName = "Anderson", Email = "daniel.anderson@example.com" },
                new Person { FirstName = "Olivia", LastName = "Taylor" } // Email is omitted
            };

            People.AddRange(people);

            SaveChanges();
        }
    }
}
