using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Core;
using PeopleManager.Model;

namespace PeopleManager.Ui.Mvc.Controllers;

public class PeopleController : Controller
{
    private readonly PeopleManagerDbContext _peopleManagerDbContext;

    public PeopleController(PeopleManagerDbContext peopleManagerDbContext)
    {
        _peopleManagerDbContext = peopleManagerDbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var people = _peopleManagerDbContext.People.ToList();

        return View(people);
    }

    // CREATE
    // GET
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST --> data verwerken
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Person person)
    {
        if (!ModelState.IsValid) return View(person);
        _peopleManagerDbContext.People.Add(person);
        _peopleManagerDbContext.SaveChanges(); //zeker doen, nu moet je het uitvoeren
        return RedirectToAction("Index");
    }

    // EDIT
    // GET
    [HttpGet]
    public IActionResult Edit([FromRoute] int id)
    {
        var person =
            _peopleManagerDbContext.People.FirstOrDefault(p =>
                p.Id == id); //als hij de persoon niet vindt, dan is het null
        if (person is null) //indien null (zo weinig mogelijk een else schrijven) //alle logica vanboven
            return RedirectToAction("Index");
        return View(person);
    }

    // POST  --> data verwerken
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult
        Edit([FromRoute] int id, [FromForm] Person person) // id moet je van de route halen, person uit het formulier
    {
        if (!ModelState.IsValid) return View(person);
        var dbPerson =
            _peopleManagerDbContext.People.FirstOrDefault(p =>
                p.Id == id); //als hij de persoon niet vindt, dan is het null
        if (dbPerson is null) //indien null (zo weinig mogelijk een else schrijven) //alle logica vanboven
            return RedirectToAction("Index");
        dbPerson.FirstName = person.FirstName;
        dbPerson.LastName = person.LastName;
        dbPerson.Email = person.Email;

        _peopleManagerDbContext.SaveChanges(); //zeker doen, nu moet je het uitvoeren

        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult Delete([FromRoute] int id)
    {
        var person =
            _peopleManagerDbContext.People.FirstOrDefault(p =>
                p.Id == id); //als hij de persoon niet vindt, dan is het null
        if (person is null) //indien null (zo weinig mogelijk een else schrijven) //alle logica vanboven
            return RedirectToAction("Index");
        return View(person);
    }

    [HttpPost]
    [Route("People/delete/{id:int?}")] //dit is de route die je moet volgen id:int --> dubbele validate- het moet een getal zijn
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var person = new Person
        {
            Id = id,
            FirstName = string.Empty,
            LastName = string.Empty
        };

        _peopleManagerDbContext.People.Attach(person);
        _peopleManagerDbContext.People.Remove(person); //markeren als je mag die verwijderen
        _peopleManagerDbContext.SaveChanges(); //het effectief verwijderen

        return RedirectToAction("Index");
    }
}