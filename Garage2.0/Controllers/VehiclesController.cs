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
using System.Data.SqlClient;

namespace Garage2._0.Controllers
{
    public class VehiclesController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Vehicles
        public ActionResult Index(string searchNumberPlate = "", string typeOfVehicle = "", string orderBy = "") //SearchNumberPlate=a&typeOfVehicle=Car&OrderBy=...
        {
            var vehicles = db.Vehicles.Include(v => v.Member).Include(v => v.VehicleType);

            ViewBag.Fordon = "fordon";
            ViewBag.TypeOfVehicle = typeOfVehicle;

            if (!string.IsNullOrEmpty(typeOfVehicle))
            {
                switch (typeOfVehicle.ToUpper())
                {
                    case "CAR": //i if-sats kan man skriva: if (typeOfVehicle.ToUpper() == TypeOfVehicle.Car.ToString().ToUpper())
                        vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Car);
                        ViewBag.Fordon = (vehicles.Count() == 1) ? "bil" : "bilar"; //borde finnas statisk metod i klassen VehicleType
                        ViewBag.TypeOfVehicle = TypeOfVehicle.Car.ToString(); //blir på korrekt format
                        break;
                    case "BUS":
                        vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Bus);
                        ViewBag.Fordon = (vehicles.Count() == 1) ? "buss" : "bussar"; //borde finnas statisk metod i klassen VehicleType
                        ViewBag.TypeOfVehicle = TypeOfVehicle.Bus.ToString(); //blir på korrekt format
                        break;
                    case "BOAT":
                        vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Boat);
                        ViewBag.Fordon = (vehicles.Count() == 1) ? "b&aring;t" : "b&aring;tar"; //borde finnas statisk metod i klassen VehicleType
                        ViewBag.TypeOfVehicle = TypeOfVehicle.Boat.ToString(); //blir på korrekt format
                        break;
                    case "AIRPLANE":
                        vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Airplane);
                        ViewBag.Fordon = "flygplan"; //borde finnas statisk metod i klassen TypeOfVehicle
                        ViewBag.TypeOfVehicle = TypeOfVehicle.Airplane.ToString(); //blir på korrekt format
                        break;
                    case "MOTORCYCLE":
                        vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Motorcycle);
                        ViewBag.Fordon = (vehicles.Count() == 1) ? "motorcykel" : "motorcyklar"; //borde finnas statisk metod i klassen VehicleType
                        ViewBag.TypeOfVehicle = TypeOfVehicle.Motorcycle.ToString(); //blir på korrekt format
                        break;
                }
                if (vehicles.Count() == 0) return HttpNotFound();
            }
            //Gamla koden:
            //if (typeOfVehicle != "")
            //{
            //    switch (typeOfVehicle.ToUpper())
            //    {
            //        case "CAR":
            //            vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Car); ViewBag.Fordon = "bilar"; ViewBag.TypeOfVehicle = "Car"; break;
            //        case "BUS":
            //            vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Bus); ViewBag.Fordon = "bussar"; ViewBag.TypeOfVehicle = "Bus"; break;
            //        case "BOAT":
            //            vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Boat); ViewBag.Fordon = "b&aring;tar"; ViewBag.TypeOfVehicle = "Boat"; break;
            //        case "AIRPLANE":
            //            vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Airplane); ViewBag.Fordon = "flygplan"; ViewBag.TypeOfVehicle = "Airplane"; break;
            //        case "MOTORCYCLE":
            //            vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Motorcycle); ViewBag.Fordon = "motorcyklar"; ViewBag.TypeOfVehicle = "Motorcycle"; break;
            //    }
            //    if (vehicles.Count() == 0) return HttpNotFound();
            //}

            if (orderBy != "")
                switch (orderBy.ToUpper())
                {
                    case "MEMBERNAME": vehicles = vehicles.OrderBy(v => v.Member.Name); break;
                    case "TYPEOFVEHICLE": vehicles = vehicles.OrderBy(v => v.VehicleType.TypeOfVehicle); break; //.TypeOfVehicle tillagt (annars blir det fel!) /Stefan 2017-06-27
                    case "REGNUMBER": vehicles = vehicles.OrderBy(v => v.RegNumber); break;
                    //case "COLOR": vehicles = vehicles.OrderBy(v => v.Color); break;
                    //case "NOOFWHEELS": vehicles = vehicles.OrderBy(v => v.NoOfWheels); break;
                    //case "BRAND": vehicles = vehicles.OrderBy(v => v.Brand); break;
                    //case "MODEL": vehicles = vehicles.OrderBy(v => v.Model); break;
                    case "CHECKINTIME": vehicles = vehicles.OrderBy(v => v.CheckInTime); break;
                }

            if (!String.IsNullOrEmpty(searchNumberPlate))
            {
                vehicles = vehicles.Where(p => p.RegNumber.StartsWith(searchNumberPlate));
            }

            List<VehicleBase> vehicleBaseList = new List<VehicleBase>();

            if (vehicles.Count() > 0)
            {
                foreach (var item in vehicles)
                {
                    var vehicleBase = new VehicleBase();
                    vehicleBase.Id = item.Id;
                    vehicleBase.MemberName = item.Member.Name;
                    vehicleBase.VehicleType = item.VehicleType.TypeOfVehicle.ToString();
                    vehicleBase.RegNumber = item.RegNumber;
                    vehicleBase.CheckInTime = item.CheckInTime;
                    vehicleBaseList.Add(vehicleBase);
                }
            }

            ViewBag.NoOfParkedVehicles = vehicles.Count();

            return View(vehicleBaseList);
        }


        // GET: Vehicles
        public ActionResult DetailedIndex(string searchNumberPlate = "", string typeOfVehicle = "", string orderBy = "") //SearchNumberPlate=a&typeOfVehicle=Car&OrderBy=...  
        {
            var vehicles = db.Vehicles.Include(v => v.Member).Include(v => v.VehicleType);

            ViewBag.Fordon = "fordon";
            ViewBag.TypeOfVehicle = typeOfVehicle;

            if (!string.IsNullOrEmpty(typeOfVehicle))
            {
                switch (typeOfVehicle.ToUpper())
                {
                    case "CAR": //i if-sats kan man skriva: if (typeOfVehicle.ToUpper() == TypeOfVehicle.Car.ToString().ToUpper())
                        vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Car);
                        ViewBag.Fordon = (vehicles.Count() == 1) ?  "bil" : "bilar"; //borde finnas statisk metod i klassen VehicleType
                        ViewBag.TypeOfVehicle = TypeOfVehicle.Car.ToString(); //blir på korrekt format
                        break;
                    case "BUS":
                        vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Bus);
                        ViewBag.Fordon = (vehicles.Count() == 1) ? "buss" : "bussar"; //borde finnas statisk metod i klassen VehicleType
                        ViewBag.TypeOfVehicle = TypeOfVehicle.Bus.ToString(); //blir på korrekt format
                        break;
                    case "BOAT":
                        vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Boat);
                        ViewBag.Fordon = (vehicles.Count() == 1) ? "b&aring;t" : "b&aring;tar"; //borde finnas statisk metod i klassen VehicleType
                        ViewBag.TypeOfVehicle = TypeOfVehicle.Boat.ToString(); //blir på korrekt format
                        break;
                    case "AIRPLANE":
                        vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Airplane);
                        ViewBag.Fordon = "flygplan"; //borde finnas statisk metod i klassen TypeOfVehicle
                        ViewBag.TypeOfVehicle = TypeOfVehicle.Airplane.ToString(); //blir på korrekt format
                        break;
                    case "MOTORCYCLE":
                        vehicles = vehicles.Where(v => v.VehicleType.TypeOfVehicle == TypeOfVehicle.Motorcycle);
                        ViewBag.Fordon = (vehicles.Count() == 1) ? "motorcykel" : "motorcyklar"; //borde finnas statisk metod i klassen VehicleType
                        ViewBag.TypeOfVehicle = TypeOfVehicle.Motorcycle.ToString(); //blir på korrekt format
                        break;
                }
                if (vehicles.Count() == 0) return HttpNotFound();
            }

            if (!string.IsNullOrEmpty(orderBy))
                switch (orderBy.ToUpper())
                {
                    case "MEMBERNAME": vehicles = vehicles.OrderBy(v => v.Member.Name); break;
                    case "TYPEOFVEHICLE": vehicles = vehicles.OrderBy(v => v.VehicleType.TypeOfVehicle); break; //.TypeOfVehicle tillagt (annars blir det fel!) /Stefan 2017-06-27
                    case "REGNUMBER": vehicles = vehicles.OrderBy(v => v.RegNumber); break;
                    case "COLOR": vehicles = vehicles.OrderBy(v => v.Color); break;
                    case "NOOFWHEELS": vehicles = vehicles.OrderBy(v => v.NoOfWheels); break;
                    case "BRAND": vehicles = vehicles.OrderBy(v => v.Brand); break;
                    case "MODEL": vehicles = vehicles.OrderBy(v => v.Model); break;
                    case "CHECKINTIME": vehicles = vehicles.OrderBy(v => v.CheckInTime); break;
                }

            if (!String.IsNullOrEmpty(searchNumberPlate))
            {
                vehicles = vehicles.Where(p => p.RegNumber.StartsWith(searchNumberPlate));
            }

            ViewBag.NoOfParkedVehicles = vehicles.Count();

            if (vehicles.Count() >0)
            {
                return View(vehicles.ToList());
            }
            return View();
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Park
        public ActionResult Park(string typeOfVehicle = "Vehicle" )
        {
            //if (db.Members.Count() == 0)
            //{
            //    //ViewBag.ErrorMessage = "Medlemslistan är tom. Registrera minst en medlem först.";
            //    return View("../Members/Create");
            //}

            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name");
            //var selected = db.VehicleTypes.Where(s => s.TypeOfVehicle == TypeOfVehicle...);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Id");

            //ViewBag.TypeOfVehicle = typeOfVehicle;

            if (typeOfVehicle.ToUpper() == TypeOfVehicle.Car.ToString().ToUpper())
            {
                ViewBag.TypeOfVehicle = TypeOfVehicle.Car.ToString();
                ViewBag.NyttFordon = "ny bil";
                ViewBag.ParkeraFordon = "bilen";
            }
            else if (typeOfVehicle.ToUpper() == TypeOfVehicle.Bus.ToString().ToUpper())
            {
                ViewBag.TypeOfVehicle = TypeOfVehicle.Bus.ToString();
                ViewBag.NyttFordon = "ny buss";
                ViewBag.ParkeraFordon = "bussen";
            }
            else if (typeOfVehicle.ToUpper().Equals(TypeOfVehicle.Boat.ToString().ToUpper()))
            {
                ViewBag.TypeOfVehicle = TypeOfVehicle.Boat.ToString();
                ViewBag.NyttFordon = "ny b&aring;t";
                ViewBag.ParkeraFordon = "b&aring;ten";
            }
            else if (typeOfVehicle.ToUpper() == TypeOfVehicle.Airplane.ToString().ToUpper())
            {
                ViewBag.TypeOfVehicle = TypeOfVehicle.Airplane.ToString();
                ViewBag.NyttFordon = "nytt flygplan";
                ViewBag.ParkeraFordon = "flygplanet";
            }
            else if (typeOfVehicle.ToUpper() == TypeOfVehicle.Motorcycle.ToString().ToUpper())
            {
                ViewBag.TypeOfVehicle = TypeOfVehicle.Motorcycle.ToString();
                ViewBag.NyttFordon = "ny motorcykel";
                ViewBag.ParkeraFordon = "motorcykeln";
            }
            else
            {
                ViewBag.TypeOfVehicle = typeOfVehicle;
                ViewBag.NyttFordon = "nytt fordon";
                ViewBag.ParkeraFordon = "fordonet";
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Park([Bind(Include = "Id,VehicleType.TypeOfVehicle,RegNumber,Color,NoOfWheels,Brand,Model")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);

            return View(vehicle);
        }


        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegNumber,Color,NoOfWheels,Brand,Model,CheckInTime,MemberId,VehicleTypeId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        //POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle Vehicle = db.Vehicles.Find(id);
            var checkOutTime = DateTime.Now;
            TimeSpan parkingDuration = checkOutTime - Vehicle.CheckInTime;
            ViewBag.CheckOutTime = checkOutTime;

            ViewBag.ParkingFee = Fee(parkingDuration);
            string pDuration;
            if (parkingDuration.Days > 0)
            {
                pDuration = $"{parkingDuration:dd} dygn {parkingDuration:hh\\:mm\\:ss} (tt:mm:ss)";
            }
            else pDuration = $"{parkingDuration:hh\\:mm\\:ss} (tt:mm:ss)";
            ViewBag.ParkingDuration = pDuration;
            ViewBag.TypeOfVehicle = Vehicle.VehicleType.TypeOfVehicle;
            db.Vehicles.Remove(Vehicle);
            db.SaveChanges();
            return View("Receipt", Vehicle);
        }

        private decimal Fee(TimeSpan parkingDuration)
        {
            if ((parkingDuration.Minutes % 10) > 0 || ((parkingDuration.Minutes % 10 == 0) && (parkingDuration.Seconds > 0)))
            {
                return 10 * ((parkingDuration.Days * 144) + (parkingDuration.Hours * 6) + (parkingDuration.Minutes / 10) + 1);
            }
            return 10 * ((parkingDuration.Days * 144) + (parkingDuration.Hours * 6) + (parkingDuration.Minutes / 10));
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

//namespace Garage2._0.Controllers
//{
//    public class ParkedVehiclesController : Controller
//    {
//        private GarageContext db = new GarageContext();

//        // GET: ParkedVehicles/Index
//        public ActionResult Index(string searchNumberPlate = "", string typeOfVehicle = "", string orderBy = "")
//        {
//            var vehicles = db.ParkedVehicles.Select(v => v);

//            ViewBag.Fordon = "fordon";
//            ViewBag.TypeOfVehicle = typeOfVehicle;

//            if (typeOfVehicle != "")
//            {
//                switch (typeOfVehicle.ToUpper())
//                {
//                    case "CAR":
//                        vehicles = vehicles.Where(v => v.TypeOfVehicle == Car); ViewBag.Fordon = "bilar"; ViewBag.TypeOfVehicle = "Car"; break;
//                    case "BUS":
//                        vehicles = vehicles.Where(v => v.TypeOfVehicle == Bus); ViewBag.Fordon = "bussar"; ViewBag.TypeOfVehicle = "Bus"; break;
//                    case "BOAT":
//                        vehicles = vehicles.Where(v => v.TypeOfVehicle == Boat); ViewBag.Fordon = "båtar"; ViewBag.TypeOfVehicle = "Boat"; break;
//                    case "AIRPLANE":
//                        vehicles = vehicles.Where(v => v.TypeOfVehicle == Airplane); ViewBag.Fordon = "flygplan"; ViewBag.TypeOfVehicle = "Airplane"; break;
//                    case "MOTORCYCLE":
//                        vehicles = vehicles.Where(v => v.TypeOfVehicle == Motorcycle); ViewBag.Fordon = "motorcyklar"; ViewBag.TypeOfVehicle = "Motorcycle"; break;
//                }
//                if (vehicles.Count() == 0) return HttpNotFound();
//            }

//            if (orderBy != "")
//                switch (orderBy.ToUpper())
//                {
//                    case "TYPEOFVEHICLE": vehicles = vehicles.OrderBy(v => v.TypeOfVehicle); break;
//                    case "REGNUMBER": vehicles = vehicles.OrderBy(v => v.RegNumber); break;
//                    case "COLOR": vehicles = vehicles.OrderBy(v => v.Color); break;
//                    case "NOOFWHEELS": vehicles = vehicles.OrderBy(v => v.NoOfWheels); break;
//                    case "BRAND": vehicles = vehicles.OrderBy(v => v.Brand); break;
//                    case "MODEL": vehicles = vehicles.OrderBy(v => v.Model); break;
//                }

//            if (!String.IsNullOrEmpty(searchNumberPlate))
//            {
//                vehicles = vehicles.Where(p => p.RegNumber.StartsWith(searchNumberPlate));
//            }

//            ViewBag.NoOfParkedVehicles = vehicles.Count();

//            return View(vehicles.ToList());
//        }

//        // GET: ParkedVehicles/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
//            if (parkedVehicle == null)
//            {
//                return HttpNotFound();
//            }
//            return View(parkedVehicle);
//        }

//        // GET: ParkedVehicles/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: ParkedVehicles/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,TypeOfVehicle,RegNumber,Color,NoOfWheels,Brand,Model,CheckInTime,CheckOutTime,ParkingDuration,ParkingFee")] ParkedVehicle parkedVehicle)
//        {
//            if (ModelState.IsValid)
//            {
//                db.ParkedVehicles.Add(parkedVehicle);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(parkedVehicle);
//        }

//        // GET: ParkedVehicles/Park
//        public ActionResult Park(string typeOfVehicle)
//        {
//            ViewBag.TypeOfVehicle = typeOfVehicle;

//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Park([Bind(Include = "Id,TypeOfVehicle,RegNumber,Color,NoOfWheels,Brand,Model")] ParkedVehicle parkedVehicle)
//        {
//            if (ModelState.IsValid)
//            {
//                db.ParkedVehicles.Add(parkedVehicle);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(parkedVehicle);
//        }

//        // GET: ParkedVehicles/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
//            if (parkedVehicle == null)
//            {
//                return HttpNotFound();
//            }
//            return View(parkedVehicle);
//        }

//        // POST: ParkedVehicles/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,TypeOfVehicle,RegNumber,Color,NoOfWheels,Brand,Model,CheckInTime,CheckOutTime,ParkingDuration,ParkingFee")] ParkedVehicle parkedVehicle)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(parkedVehicle).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(parkedVehicle);
//        }

//        // GET: ParkedVehicles/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
//            if (parkedVehicle == null)
//            {
//                return HttpNotFound();
//            }
//            return View(parkedVehicle);
//        }

//        // POST: ParkedVehicles/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
//            var checkOutTime = DateTime.Now;
//            TimeSpan parkingDuration = checkOutTime - parkedVehicle.CheckInTime;
//            ViewBag.CheckOutTime = checkOutTime;

//            ViewBag.ParkingFee = Fee(parkingDuration);
//            string pDuration;
//            if (parkingDuration.Days > 0)
//            {
//                pDuration = $"{parkingDuration:dd} dygn {parkingDuration:HH\\:mm\\:ss} (tt:mm:ss)";
//            }
//            else pDuration = $"{parkingDuration:hh\\:mm\\:ss} (tt:mm:ss)";
//            ViewBag.ParkingDuration = pDuration;

//            db.ParkedVehicles.Remove(parkedVehicle);
//            db.SaveChanges();
//            return View("Receipt", parkedVehicle);
//        }

//        private decimal Fee(TimeSpan parkingDuration)
//        {
//            if ((parkingDuration.Minutes % 10) > 0 || ((parkingDuration.Minutes % 10 == 0) && (parkingDuration.Seconds > 0)))
//            {
//                return 10 * ((parkingDuration.Days * 144) + (parkingDuration.Hours * 6) + (parkingDuration.Minutes / 10) + 1);
//            }
//            return 10 * ((parkingDuration.Days * 144) + (parkingDuration.Hours * 6) + (parkingDuration.Minutes / 10));
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
