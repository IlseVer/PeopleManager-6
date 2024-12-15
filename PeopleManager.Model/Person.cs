using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleManager.Model
{
    [Table(nameof(Person))]
    public class Person
    {
        public int Id { get; set; } // Primary Key

        [Required] // Required voor UI
        [DisplayName("First Name")] // ales mijn naam van mijn property gerenderd moet worden op het scherm
        [StringLength(100)] // max 50 characters
        public required string FirstName { get; set; } //required voor back-end

        [Required]
        [DisplayName("Last Name")]
        public required string LastName { get; set; }

        [DisplayName("E-mail address")]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
