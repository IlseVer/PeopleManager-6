﻿using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Core;
using PeopleManager.Ui.Mvc.Models;

namespace PeopleManager.Ui.Mvc.Controllers
{
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
				_peopleManagerDbContext.People.Add(person);
				_peopleManagerDbContext.SaveChanges();  //zeker doen, nu moet je het uitvoeren
				return RedirectToAction("Index");
		}

        // EDIT
        // GET
        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var person = _peopleManagerDbContext.People.FirstOrDefault(p => p.Id == id); //als hij de persoon niet vindt, dan is het null
            if (person is null)  //indien null (zo weinig mogelijk een else schrijven) //alle logica vanboven
            {
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // POST  --> data verwerken
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, [FromForm]Person person)// id moet je van de route halen, person uit het formulier
        {
            var dbPerson = _peopleManagerDbContext.People.FirstOrDefault(p => p.Id == id); //als hij de persoon niet vindt, dan is het null
            if (dbPerson is null)  //indien null (zo weinig mogelijk een else schrijven) //alle logica vanboven
            {
                return RedirectToAction("Index");
            }
            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;

            _peopleManagerDbContext.SaveChanges();  //zeker doen, nu moet je het uitvoeren

            return RedirectToAction("Index");
        }
    }
}