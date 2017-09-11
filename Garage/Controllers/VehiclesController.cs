using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Garage.DataAccess;
using Garage.Models;

namespace Garage.Controllers
{
    public class VehiclesController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Vehicles
        public ActionResult Index(int memberId = 0, string memberName = "", string searchNumberPlate = "", string typeOfVehicle = "", string orderBy = "") //, string detailed
        {
            var vehicles = db.Vehicles.Include(v => v.Member).Include(v => v.VehicleType);

            ViewBag.MemberName = "";

            if (memberId > 0)
            {
                ViewBag.MemberId = memberId;
                ViewBag.MemberName = db.Members.Find(memberId).Name;
                //ViewBag.MedlemHar = ViewBag.MemberName + " har ";
                vehicles = vehicles.Where(v => v.MemberId == memberId);
            }
            else if (!string.IsNullOrEmpty(memberName))
            {
                ViewBag.MemberId = db.Members.ToList().Find(m => m.Name == memberName).Id; //<----------------
                ViewBag.MemberName = memberName;
                //ViewBag.MedlemHar = memberName + " har ";
                vehicles = vehicles.Where(v => v.Member.Name == memberName);
            }

            if (!String.IsNullOrEmpty(searchNumberPlate))
            {
                vehicles = vehicles.Where(p => p.RegNumber.StartsWith(searchNumberPlate));
            }

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

            if (orderBy != "")
                switch (orderBy.ToUpper())
                {
                    case "MEMBERNAME": vehicles = vehicles.OrderBy(v => v.Member.Name); /*ViewBag.orderBy = "MemberName";*/  break;
                    case "TYPEOFVEHICLE": vehicles = vehicles.OrderBy(v => v.VehicleType.TypeOfVehicle); ViewBag.orderBy = "TypeOfVehicle"; break; //.TypeOfVehicle tillagt (annars blir det fel!) /Stefan 2017-06-27
                    case "REGNUMBER": vehicles = vehicles.OrderBy(v => v.RegNumber); ViewBag.orderBy = "RegNumber"; break;
                    case "CHECKINTIME": vehicles = vehicles.OrderBy(v => v.CheckInTime); ViewBag.orderBy = "CheckInTime"; break;
                }
            else vehicles = vehicles.OrderBy(v => v.Member.Name);

            List<VehicleBase> vehicleBaseList = new List<VehicleBase>();

            if (vehicles.Count() > 0)
            {
                foreach (var vehicle in vehicles)
                {
                    var vehicleBase = new VehicleBase();
                    vehicleBase.Id = vehicle.Id;
                    vehicleBase.MemberName = vehicle.Member.Name;
                    vehicleBase.MemberId = vehicle.Member.Id;
                    vehicleBase.VehicleType = vehicle.VehicleType.TypeOfVehicle.ToString();
                    vehicleBase.RegNumber = vehicle.RegNumber;
                    vehicleBase.CheckInTime = vehicle.CheckInTime;
                    vehicleBaseList.Add(vehicleBase);
                }
            }

            ViewBag.NoOfParkedVehicles = vehicles.Count();

            return View(vehicleBaseList);
        }


        // GET: Vehicles
        public ActionResult DetailedIndex(int memberId = 0, string memberName = "", string searchNumberPlate = "", string typeOfVehicle = "", string orderBy = "")
        {
            var vehicles = db.Vehicles.Include(v => v.Member).Include(v => v.VehicleType);

            //if (memberId > 0) vehicles = vehicles.Where(v => v.MemberId == memberId);
            ViewBag.MemberName = "";

            if (memberId > 0)
            {
                ViewBag.MemberId = memberId;
                ViewBag.MemberName = db.Members.Find(memberId).Name;
                //ViewBag.MedlemHar = ViewBag.MemberName + " har ";
                vehicles = vehicles.Where(v => v.MemberId == memberId);
            }
            else if (!string.IsNullOrEmpty(memberName))
            {
                ViewBag.MemberId = db.Members.ToList().Find(m => m.Name == memberName).Id;
                ViewBag.MemberName = memberName;
                //ViewBag.MedlemHar = memberName + " har ";
                vehicles = vehicles.Where(v => v.Member.Name == memberName);
            }

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
                    case "MEMBERNAME": vehicles = vehicles.OrderBy(v => v.Member.Name); /*ViewBag.orderBy = "MemberName";*/ break;
                    case "TYPEOFVEHICLE": vehicles = vehicles.OrderBy(v => v.VehicleType.TypeOfVehicle); ViewBag.orderBy = "TypeOfVehicle"; break; //.TypeOfVehicle tillagt (annars blir det fel!) /Stefan 2017-06-27
                    case "REGNUMBER": vehicles = vehicles.OrderBy(v => v.RegNumber); ViewBag.orderBy = "RegNumber"; break;
                    case "COLOR": vehicles = vehicles.OrderBy(v => v.Color); break;
                    case "NOOFWHEELS": vehicles = vehicles.OrderBy(v => v.NoOfWheels); break;
                    case "BRAND": vehicles = vehicles.OrderBy(v => v.Brand); break;
                    case "MODEL": vehicles = vehicles.OrderBy(v => v.Model); break;
                    case "CHECKINTIME": vehicles = vehicles.OrderBy(v => v.CheckInTime); ViewBag.orderBy = "CheckInTime"; break;
                }
            else vehicles = vehicles.OrderBy(v => v.Member.Name);

            if (!String.IsNullOrEmpty(searchNumberPlate))
            {
                vehicles = vehicles.Where(p => p.RegNumber.StartsWith(searchNumberPlate));
            }

            ViewBag.NoOfParkedVehicles = vehicles.Count();

            if (vehicles.Count() > 0)
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
            else
            {
                switch (vehicle.VehicleType.TypeOfVehicle)
                {
                    case TypeOfVehicle.Car: ViewBag.Fordonstyp = "Bil"; break;
                    case TypeOfVehicle.Bus: ViewBag.Fordonstyp = "Buss"; break;
                    case TypeOfVehicle.Boat: ViewBag.Fordonstyp = "Båt"; break;
                    case TypeOfVehicle.Airplane: ViewBag.Fordonstyp = "Flygplan"; break;
                    case TypeOfVehicle.Motorcycle: ViewBag.Fordonstyp = "Motorcykel"; break;
                    default: ViewBag.Fordonstyp = "Fordon"; break;
                }
            }

            return View(vehicle);
        }

        // GET: Vehicles/Park
        public ActionResult Park(int memberId = 0, string memberName = "", string typeOfVehicle = "Vehicle")
        {
            int MemberId = memberId;
            ViewBag.MemberIDNr = memberId;
            if (MemberId > 0)
            {
                ViewBag.MemberName = db.Members.Where(m => m.Id == memberId).FirstOrDefault().Name;
            }

            if (!string.IsNullOrEmpty(memberName))
            {
                ViewBag.MemberName = memberName;
                MemberId = db.Members.Find(memberName).Id;
                ViewBag.MemberIdNr = memberId;
            }

            if (MemberId > 0) ViewBag.MemberId = new SelectList(db.Members.Include(m => m.Vehicles).Where(m2 => m2.Id == MemberId).OrderBy(m => m.Name), "Id", "Name", MemberId); //<-------tillagt
            else ViewBag.MemberId= new SelectList(db.Members.Include(m => m.Vehicles).OrderBy(m => m.Name), "Id", "Name");           //----------------------Include behövs?
            //ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes.Include(vt => vt.TypeOfVehicle), "Id", "Id"); //<---------------------Include behövs?

            if (typeOfVehicle.ToUpper() == TypeOfVehicle.Car.ToString().ToUpper())
            {
                //ViewBag.TypeOfVehicle = TypeOfVehicle.Car.ToString();
                ViewBag.NyttFordon = "ny bil";
                ViewBag.ParkeraFordon = "bilen";
                ViewBag.AntalHjul = 4;
                //ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes.Include(vt => vt.TypeOfVehicle).Where(tv => tv.TypeOfVehicle == TypeOfVehicle.Car), "Id", "Id", TypeOfVehicle.Car);
            }
            else if (typeOfVehicle.ToUpper() == TypeOfVehicle.Bus.ToString().ToUpper())
            {
                //ViewBag.TypeOfVehicle = TypeOfVehicle.Bus.ToString();
                ViewBag.NyttFordon = "ny buss";
                ViewBag.ParkeraFordon = "bussen";
                ViewBag.AntalHjul = 4;
                ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes.Include(vt => vt.TypeOfVehicle).Where(tv => tv.TypeOfVehicle == TypeOfVehicle.Bus), "Id", "Id", TypeOfVehicle.Bus);
            }
            else if (typeOfVehicle.ToUpper().Equals(TypeOfVehicle.Boat.ToString().ToUpper()))
            {
                //ViewBag.TypeOfVehicle = TypeOfVehicle.Boat.ToString();
                ViewBag.NyttFordon = "ny båt"; 
                ViewBag.ParkeraFordon = "b&#229;ten";
                ViewBag.AntalHjul = 0;
                //ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes.Include(vt => vt.TypeOfVehicle).Where(tv => tv.TypeOfVehicle == TypeOfVehicle.Boat), "Id", "Id", TypeOfVehicle.Boat);
            }
            else if (typeOfVehicle.ToUpper() == TypeOfVehicle.Airplane.ToString().ToUpper())
            {
                //ViewBag.TypeOfVehicle = TypeOfVehicle.Airplane.ToString();
                ViewBag.NyttFordon = "nytt flygplan";
                ViewBag.ParkeraFordon = "flygplanet";
                ViewBag.AntalHjul = 8;
                //ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes.Include(vt => vt.TypeOfVehicle).Where(tv => tv.TypeOfVehicle == TypeOfVehicle.Airplane), "Id", "Id", TypeOfVehicle.Airplane);
            }
            else if (typeOfVehicle.ToUpper() == TypeOfVehicle.Motorcycle.ToString().ToUpper())
            {
                //ViewBag.TypeOfVehicle = TypeOfVehicle.Motorcycle.ToString();
                ViewBag.NyttFordon = "ny motorcykel";
                ViewBag.ParkeraFordon = "motorcykeln";
                ViewBag.AntalHjul = 2;
                //ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes.Include(vt => vt.TypeOfVehicle).Where(tv => tv.TypeOfVehicle == TypeOfVehicle.Motorcycle), "Id", "Id");
            }
            else
            {
                //ViewBag.TypeOfVehicle = typeOfVehicle;
                ViewBag.NyttFordon = "nytt fordon";
                ViewBag.ParkeraFordon = "fordonet";
                //ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes.Include(vt => vt.TypeOfVehicle), "Id", "Id", "");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Park([Bind(Include = "Id,RegNumber,Color,NoOfWheels,Brand,Model,CheckInTime,MemberId,VehicleTypeId")] Vehicle vehicle, [Bind(Include = "Id,TypeOfVehicle")] VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.VehicleTypes.Add(vehicleType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", vehicle.MemberId);
            //ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);

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
            else
            {
                switch (vehicle.VehicleType.TypeOfVehicle)
                { 
                    case TypeOfVehicle.Car: ViewBag.Fordonstyp = "Bil"; break;
                    case TypeOfVehicle.Bus: ViewBag.Fordonstyp = "Buss"; break;
                    case TypeOfVehicle.Boat: ViewBag.Fordonstyp = "Båt"; break;
                    case TypeOfVehicle.Airplane: ViewBag.Fordonstyp = "Flygplan"; break;
                    case TypeOfVehicle.Motorcycle: ViewBag.Fordonstyp = "Motorcykel"; break;
                    default: ViewBag.Fordonstyp = "Fordon"; break;
                }
            }

            ViewBag.MemberId = new SelectList(db.Members.OrderBy(m => m.Name), "Id", "Name", vehicle.MemberId);
            //ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Id", vehicle.VehicleType.TypeOfVehicle);
            //ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes.Include(vt => vt.TypeOfVehicle), "Id", "Id", vehicle.VehicleType.TypeOfVehicle);
            return View(vehicle);
        }

        //POST: Vehicles/Edit/5
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
                return RedirectToAction("Details", new { id = vehicle.Id }); //<------ändrat!!
            }
            //ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", vehicle.MemberId);
            //ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        //public ActionResult Edit([Bind(Include = "Id,RegNumber,Color,NoOfWheels,Brand,Model")] Vehicle vehicle, [Bind(Include = "Id,MemberId")] Member member) //,CheckInTime ,MemberId, VehicleTypeId" , [Bind(Include = "Id,TypeOfVehicle")]] VehicleType vehicleType
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(member).State = EntityState.Modified;
        //        db.Entry(vehicle).State = EntityState.Modified;

        //        db.SaveChanges();
               
        //        return RedirectToAction("Index");
        //    }
        //    return View(vehicle);
        //}

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
