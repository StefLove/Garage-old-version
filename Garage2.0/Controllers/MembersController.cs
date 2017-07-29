using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage2._0.DataAccess;
using Garage2._0.Models;

namespace Garage2._0.Controllers
{
    public class MembersController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Members
        //public ActionResult Index()
        //{
        //    return View(db.Members.ToList());
        //}

        //public ActionResult Index(string searchMember)
        //{
        //    var member = from m in db.Members select m;

        //    if (!String.IsNullOrEmpty(searchMember))
        //    {
        //        member = member.Where(p => p.Name.StartsWith(searchMember));
        //        return View(member);
        //    }
        //    return View(db.Members.ToList());
        //}

        //GET: Members/Details/5

        public ActionResult Index(string option, string search)
        {
            var members = db.Members; //.Include(m => m.Vehicles);

            if (option == "Name")
            {
                if (!String.IsNullOrEmpty(search))
                {
                    var member = members.Where(p => p.Name.StartsWith(search));
                    return View(member);
                }
                //return View(db.Members.Include(m => m.Vehicles).ToList());
            }
            else
            {
                if (!String.IsNullOrEmpty(search))
                {
                    var member = members.Where(p => p.Membershipnumber.StartsWith(search));
                    return View(member);
                }
                //return View(db.Members.Include(m => m.Vehicles).ToList());
            }
            return View(members.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Membershipnumber,Phonenumber,Adress")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Membershipnumber,Phonenumber,Adress")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);

            foreach (var vehicle in member.Vehicles) //<-------fordonen bort också!
            {
                db.Vehicles.Remove(vehicle);
            }
            db.Members.Remove(member);
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
