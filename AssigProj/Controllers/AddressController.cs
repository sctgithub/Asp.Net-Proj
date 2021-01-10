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

namespace AssigProj.Controllers
{
    public class AddressController : Controller
    {
        private ProjContext db = new ProjContext();

        // GET: Address
        public ActionResult Index()
        {
            var addresses = db.Addresses.Include(a => a.City).Include(a => a.Country).Include(a => a.Person);
            return View(addresses.ToList());
        }

        // GET: Address/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        public ActionResult GetCities(int countryid)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items = db.Cities.Where(c => c.Country.CountryId == countryid).Select(c => new SelectListItem() { Text = c.CityName, Value = c.CityId.ToString() }).ToList();

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        // GET: Address/Create
        public ActionResult Create(int? id)
        {
            //ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName");
            ViewBag.CityId = new SelectList("", "CityId", "CityName");
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName");
            ViewBag.PersonId = new SelectList(db.People, "PersonId", "Name", id);
            ViewBag.Person = db.People.Find(id);
            ViewBag.Id = id;

            //PersonCreateViewModel personCreateViewModel = new PersonCreateViewModel();
            //personCreateViewModel.countries = db.Countries.ToList();
            //personCreateViewModel.cities = new List<City>();//db.Cities.ToList();

            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressId,HouseNo,Line1,Line2,CountryId,CityId,PersonId")] Address address)
        {
            //additional validation to check correct county is selected for selected city           
            if (address.CountryId!=db.Cities.Where(c=>c.CityId==address.CityId).Select(c=>c.Country.CountryId).FirstOrDefault())
            {
                ModelState.AddModelError("CountryId", "Invalid Country Selected.");
            }
            
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Details","Person",new { id=address.PersonId});
            }

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", address.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", address.CountryId);
            ViewBag.PersonId = new SelectList(db.People, "PersonId", "Name", address.PersonId);
            ViewBag.Person = db.People.Find(address.PersonId);
            return View(address);
        }

        // GET: Address/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", address.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", address.CountryId);
            ViewBag.PersonId = new SelectList(db.People, "PersonId", "Name", address.PersonId);
            ViewBag.Person = db.People.Find(address.PersonId);
            ViewBag.Id = address.PersonId;
            return View(address);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressId,HouseNo,Line1,Line2,CountryId,CityId,PersonId")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Person", new { id = address.PersonId });
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", address.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", address.CountryId);
            ViewBag.PersonId = new SelectList(db.People, "PersonId", "Name", address.PersonId);
            return View(address);
        }

        // GET: Address/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id = address.PersonId;
            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
            db.SaveChanges();
            return RedirectToAction("Details", "Person", new { id = address.PersonId });
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
