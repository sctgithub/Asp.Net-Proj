using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssigProj.Context;
using AssigProj.Models;
using AssigProj.ViewModels;

namespace AssigProj.Controllers
{
    public class PersonController : Controller
    {
        private ProjContext db = new ProjContext();

        // GET: Person
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        [HttpPost]
        public ActionResult Index(string option, string search)
        {            
            if (!string.IsNullOrEmpty(option) && option.Trim()=="Name" && !string.IsNullOrEmpty(search))
            {
                return View(db.People.Where(p => p.Name.Contains(search)).ToList());
            }
            else if (!string.IsNullOrEmpty(option) && option.Trim()== "Phone" && !string.IsNullOrEmpty(search))
            {
                return View(db.People.Where(p => p.Phone.Contains(search)).ToList());
            }
            else if (!string.IsNullOrEmpty(option) && option.Trim()== "Email" && !string.IsNullOrEmpty(search))
            {
                return View(db.People.Where(p => p.Email.Contains(search)).ToList());
            }
            else
            {
                return View(db.People.ToList());
            }
        }

        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PersonDetailViewModel personDetailViewModel = new PersonDetailViewModel();
            personDetailViewModel.person = db.People.Find(id);
            personDetailViewModel.address = db.Addresses.Where(a => a.PersonId == id).ToList();
            personDetailViewModel.countries = db.Countries.ToList();
            personDetailViewModel.cities = new List<City>();

            if (personDetailViewModel == null)
            {
                return HttpNotFound();
            }

            return View(personDetailViewModel);

            //Person person = db.People.Find(id);
            //if (person == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(person);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            //PersonCreateViewModel personCreateViewModel = new PersonCreateViewModel();
            //personCreateViewModel.countries = db.Countries.ToList();
            //personCreateViewModel.cities = new List<City>();//db.Cities.ToList();

            //return View(personCreateViewModel);
            return View();
        }

        //public ActionResult GetCities(int countryid)
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    items = db.Cities.Where(c => c.Country.CountryId == countryid).Select(c => new SelectListItem() { Text = c.CityName, Value=c.CityId.ToString() }).ToList();
           
        //    return Json(items, JsonRequestBehavior.AllowGet);           
        //}

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonId,Name,DOB,Email,Phone")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonId,Name,DOB,Email,Phone")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
            db.Addresses.RemoveRange(db.Addresses.Where(a => a.PersonId == id).ToList());
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
