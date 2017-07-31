using System;
//using System.Collections.Generic;
//using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Garage.DataAccess;
using Garage.Models;

namespace Garage.Controllers
{
    public class VehicleTypesController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: VehicleTypes
        public ActionResult Index()
        {
            return View(db.VehicleTypes.ToList());
        }

        // GET: VehicleTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleType vehicleType = db.VehicleTypes.Find(id);
            if (vehicleType == null)
            {
                return HttpNotFound();
            }
            return View(vehicleType);
        }

        // GET: VehicleTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeOfVehicle")] VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                db.VehicleTypes.Add(vehicleType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleType);
        }

        // GET: VehicleTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleType vehicleType = db.VehicleTypes.Find(id);
            if (vehicleType == null)
            {
                return HttpNotFound();
            }
            return View(vehicleType);
        }

        // POST: VehicleTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeOfVehicle")] VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleType);
        }

        // GET: VehicleTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleType vehicleType = db.VehicleTypes.Find(id);
            if (vehicleType == null)
            {
                return HttpNotFound();
            }
            return View(vehicleType);
        }

        //// POST: ParkedVehicles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Vehicle parkedVehicle = db.Vehicles.Find(id);
        //    var checkOutTime = DateTime.Now;
        //    TimeSpan parkingDuration = checkOutTime - parkedVehicle.CheckInTime;
        //    ViewBag.CheckOutTime = checkOutTime;

        //    ViewBag.ParkingFee = Fee(parkingDuration).ToString("F");
        //    string pDuration;
        //    if (parkingDuration.Days > 0)
        //    {
        //        pDuration = $"{parkingDuration:dd} dygn {parkingDuration:hh\\:mm\\:ss} (tt:mm:ss)";
        //    }
        //    else pDuration = $"{parkingDuration:hh\\:mm\\:ss} (tt:mm:ss)";
        //    ViewBag.ParkingDuration = pDuration;

        //    db.Vehicles.Remove(parkedVehicle);
        //    db.SaveChanges();
        //    return View("Receipt", parkedVehicle);
        //}

        //private decimal Fee(TimeSpan parkingDuration)
        //{
        //    if ((parkingDuration.Minutes % 10) > 0 || ((parkingDuration.Minutes % 10 == 0) && (parkingDuration.Seconds > 0)))
        //    {
        //        return 10 * ((parkingDuration.Days * 144) + (parkingDuration.Hours * 6) + (parkingDuration.Minutes / 10) + 1);
        //    }
        //    return 10 * ((parkingDuration.Days * 144) + (parkingDuration.Hours * 6) + (parkingDuration.Minutes / 10));
        //}


        //// POST: VehicleTypes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    VehicleType vehicleType = db.VehicleTypes.Find(id);
        //    db.VehicleTypes.Remove(vehicleType);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
