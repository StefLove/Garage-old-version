namespace Garage.Migrations
{
    using System;
    using static System.AppDomain;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;
    using System.Web.Hosting;
    using Garage.Models;
    using Garage.DataAccess;
    using static Models.TypeOfVehicle;
    using System.Collections.Generic;
    //using Microsoft.Office;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using System.Reflection;
    using System.IO;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage.DataAccess.GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GarageContext context)
        {
            try
            {
                XDocument xdocFromFile = XDocument.Load(CurrentDomain.BaseDirectory.Replace(@"bin\", @"Migrations\Garage_Seed.xml"));

                // Medlemmar
                var memberXList = xdocFromFile.Root.Elements().ElementAt(3);

                if (memberXList.FirstAttribute.Value.ToUpper() == "MEDLEMMAR" &&
                    memberXList.Elements().First().Elements().ElementAt(4).Elements().First().Value.ToUpper() == "NAMN" &&
                    memberXList.Elements().First().Elements().ElementAt(4).Elements().ElementAt(1).Value.ToUpper() == "MEDLEMSNUMMER" &&
                    memberXList.Elements().First().Elements().ElementAt(4).Elements().ElementAt(2).Value.ToUpper() == "TELEFONNUMMER" &&
                    memberXList.Elements().First().Elements().ElementAt(4).Elements().ElementAt(3).Value.ToUpper() == "ADRESS")
                {
                    var members = new List<Member>();
                    Member newMember = null;

                    for (var i = 5; i < memberXList.Elements().First().Elements().Count(); i++)
                    {
                        newMember = new Member();

                        newMember.Name = memberXList.Elements().First().Elements().ElementAt(i).Elements().First().Value;
                        newMember.Membershipnumber = memberXList.Elements().First().Elements().ElementAt(i).Elements().ElementAt(1).Value;
                        newMember.Phonenumber = memberXList.Elements().First().Elements().ElementAt(i).Elements().ElementAt(2).Value;
                        newMember.Address = memberXList.Elements().First().Elements().ElementAt(i).Elements().ElementAt(3).Value;

                        members.Add(newMember);
                    }
                    members.ForEach(m => context.Members.AddOrUpdate(m));
                    context.SaveChanges();
                }

                // Fordonstyper
                var vehicleTypeXList = xdocFromFile.Root.Elements().ElementAt(4);

                if (vehicleTypeXList.FirstAttribute.Value.ToUpper() == "FORDONSTYP" &&
                    vehicleTypeXList.Elements().First().Elements().ElementAt(2).Elements().First().Value.ToUpper() == "ID" &&
                    vehicleTypeXList.Elements().First().Elements().ElementAt(2).Elements().ElementAt(1).Value.ToUpper() == "TYPAVFORDON")
                {
                    var vehicleTypes = new List<VehicleType>();
                    VehicleType newVehicleType = null;

                    for (var i = 3; i < vehicleTypeXList.Elements().First().Elements().Count(); i++)
                    {
                        newVehicleType = new VehicleType();

                        newVehicleType.Id = int.Parse(vehicleTypeXList.Elements().First().Elements().ElementAt(i).Elements().First().Value);

                        switch (vehicleTypeXList.Elements().First().Elements().ElementAt(i).Elements().ElementAt(1).Value.ToUpper())
                        {
                            case "BIL": newVehicleType.TypeOfVehicle = TypeOfVehicle.Car; break;
                            case "BUSS": newVehicleType.TypeOfVehicle = TypeOfVehicle.Bus; break;
                            case "BÅT": newVehicleType.TypeOfVehicle = TypeOfVehicle.Boat; break;
                            case "FLYGPLAN": newVehicleType.TypeOfVehicle = TypeOfVehicle.Airplane; break;
                            case "MOTORCYKEL": newVehicleType.TypeOfVehicle = TypeOfVehicle.Motorcycle; break;
                            default: newVehicleType.TypeOfVehicle = TypeOfVehicle.Car; break; //om felskrivet så får det antas vara en bil
                        }

                        vehicleTypes.Add(newVehicleType);
                    }
                    vehicleTypes.ForEach(vt => context.VehicleTypes.AddOrUpdate(vt));
                    context.SaveChanges();
                }
                // Fordon
                var vehicleXList = xdocFromFile.Root.Elements().ElementAt(5);

                if (vehicleXList.FirstAttribute.Value.ToUpper() == "FORDON" &&
                    vehicleXList.Elements().First().Elements().ElementAt(7).Elements().First().Value.ToUpper() == "FORDONSTYPID" &&
                    vehicleXList.Elements().First().Elements().ElementAt(7).Elements().ElementAt(1).Value.ToUpper() == "REGNUMMER" &&
                    vehicleXList.Elements().First().Elements().ElementAt(7).Elements().ElementAt(2).Value.ToUpper() == "MÄRKE" &&
                    vehicleXList.Elements().First().Elements().ElementAt(7).Elements().ElementAt(3).Value.ToUpper() == "MODELL" &&
                    vehicleXList.Elements().First().Elements().ElementAt(7).Elements().ElementAt(4).Value.ToUpper() == "FÄRG" &&
                    vehicleXList.Elements().First().Elements().ElementAt(7).Elements().ElementAt(5).Value.ToUpper() == "ANTALHJUL" &&
                    vehicleXList.Elements().First().Elements().ElementAt(7).Elements().ElementAt(6).Value.ToUpper() == "MEDLEMID")
                {
                    var vehicles = new List<Vehicle>();
                    Vehicle newVehicle = null;

                    for (var i = 8; i < vehicleXList.Elements().First().Elements().Count(); i++)
                    {
                        newVehicle = new Vehicle();

                        newVehicle.VehicleTypeId = int.Parse(vehicleXList.Elements().First().Elements().ElementAt(i).Elements().First().Value);
                        newVehicle.RegNumber = vehicleXList.Elements().First().Elements().ElementAt(i).Elements().ElementAt(1).Value.ToUpper();
                        newVehicle.Brand = vehicleXList.Elements().First().Elements().ElementAt(i).Elements().ElementAt(2).Value;
                        newVehicle.Model = vehicleXList.Elements().First().Elements().ElementAt(i).Elements().ElementAt(3).Value;
                        newVehicle.Color = vehicleXList.Elements().First().Elements().ElementAt(i).Elements().ElementAt(4).Value;
                        newVehicle.NoOfWheels = int.Parse(vehicleXList.Elements().First().Elements().ElementAt(i).Elements().ElementAt(5).Value);
                        newVehicle.MemberId = int.Parse(vehicleXList.Elements().First().Elements().ElementAt(i).Elements().ElementAt(6).Value);

                        vehicles.Add(newVehicle);
                    }
                    vehicles.ForEach(v => context.Vehicles.AddOrUpdate(v));
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                //throw;
                //DefaultSeed(context);
            }
        }

        void DefaultSeed(GarageContext context)
        {
            var members = new List<Member>
            {
                new Member { Name = "Adam", Membershipnumber = "1111", Phonenumber = "070 555 666", Address = "City" },
                new Member { Name = "Arch", Membershipnumber = "1211", Phonenumber = "071 555 666", Address = "City" },
                new Member { Name = "Vega", Membershipnumber = "1131", Phonenumber = "072 555 666", Address = "City" },
                new Member { Name = "Lena", Membershipnumber = "1151", Phonenumber = "073 555 666", Address = "City" },
                new Member { Name = "Filip", Membershipnumber = "1611", Phonenumber = "076 555 666", Address = "City" },
                new Member { Name = "Karl", Membershipnumber = "1811", Phonenumber = "077 555 667", Address = "City" },
                new Member { Name = "Lena", Membershipnumber = "6734", Phonenumber = "070 555 686", Address = "City" },
                new Member { Name = "Lida", Membershipnumber = "3426", Phonenumber = "070 555 866", Address = "City" },
            };

            members.ForEach(m => context.Members.AddOrUpdate(m));
            context.SaveChanges();

            var vehicleTypes = new List<VehicleType>
            {
                new VehicleType { Id = 1, TypeOfVehicle = Car },
                new VehicleType { Id = 2, TypeOfVehicle = Car },
                new VehicleType { Id = 3, TypeOfVehicle = Car },

                new VehicleType { Id = 4, TypeOfVehicle = Bus },
                new VehicleType { Id = 5, TypeOfVehicle = Boat },
                new VehicleType { Id = 6, TypeOfVehicle = Airplane },

                new VehicleType { Id = 7, TypeOfVehicle = Car },
                new VehicleType { Id = 8, TypeOfVehicle = Motorcycle },
                new VehicleType { Id = 9, TypeOfVehicle = Airplane },

                new VehicleType { Id = 10, TypeOfVehicle = Bus },
                new VehicleType { Id = 11, TypeOfVehicle = Boat },
                new VehicleType { Id = 12, TypeOfVehicle = Airplane },

                new VehicleType { Id = 13, TypeOfVehicle = Airplane }
            };
            vehicleTypes.ForEach(vt => context.VehicleTypes.AddOrUpdate(vt));
            context.SaveChanges();

            var vehicles = new List<Vehicle>
            {
                new Vehicle { VehicleTypeId = 1, RegNumber = "QSW098", Brand = "BMW", Model = "520d", Color = "Svart", NoOfWheels = 4, CheckInTime = DateTime.Now, MemberId = 1 },
                new Vehicle { VehicleTypeId = 2, RegNumber = "EDC321", Brand = "FIAT", Model = "500L", Color = "Gul", NoOfWheels = 4, CheckInTime = DateTime.Now, MemberId = 1 },

                new Vehicle { VehicleTypeId = 3, RegNumber = "AQR564", Brand = "VW", Model = "Passat", Color = "Silver", NoOfWheels = 4, CheckInTime = DateTime.Now, MemberId = 2 },
                new Vehicle { VehicleTypeId = 4, RegNumber = "DSL675", Brand = "Scania", Model = "DC13", Color = "Vit", NoOfWheels = 6, CheckInTime = DateTime.Now, MemberId = 2 },

                new Vehicle { VehicleTypeId = 5, RegNumber = "ADKN456", Brand = "Flipper", Model = "F800R", Color = "Blå", NoOfWheels = 0, CheckInTime = DateTime.Now, MemberId = 3 },

                new Vehicle { VehicleTypeId = 6, RegNumber = "NAB900", Brand = "Cessna", Model = "402", Color = "Vit", NoOfWheels = 4, CheckInTime = DateTime.Now, MemberId = 4 },
                new Vehicle { VehicleTypeId = 7, RegNumber = "GHJ789", Brand = "BMW", Model = "F800R", Color = "Svart", NoOfWheels = 2, CheckInTime = DateTime.Now, MemberId = 4 },

                new Vehicle { VehicleTypeId = 8, RegNumber = "AHJ090", Brand = "Harley-Davidson", Model = "500L", Color = "Gul", NoOfWheels = 2, CheckInTime = DateTime.Now, MemberId = 5 },

                new Vehicle { VehicleTypeId = 9, RegNumber = "JBB007", Brand = "Concorde", Model = "C33", Color = "Vit", NoOfWheels = 16, CheckInTime = DateTime.Now, MemberId = 6 },

                new Vehicle { VehicleTypeId = 10, RegNumber = "TUR333", Brand = "Scania", Model = "S2000", Color = "Vit", NoOfWheels = 8, CheckInTime = DateTime.Now, MemberId = 7 },

                new Vehicle { VehicleTypeId = 11, RegNumber = "SNE897", Brand = "Flipper", Model = "F800R", Color = "Blå", NoOfWheels = 0, CheckInTime = DateTime.Now, MemberId = 8 },
                new Vehicle { VehicleTypeId = 12, RegNumber = "DIN999", Brand = "Cessna", Model = "402", Color = "Vit", NoOfWheels = 2, CheckInTime = DateTime.Now, MemberId = 8 },
                new Vehicle { VehicleTypeId = 13, RegNumber = "QTY555", Brand = "Boeing", Model = "747", Color = "Vit", NoOfWheels = 20, CheckInTime = DateTime.Now, MemberId = 8 }

                //new Models.ParkedVehicle { TypeOfVehicle = Car, RegNumber = "FRU752", Brand = "Ferrari", Model = "F12", Color = "Röd", NoOfWheels = 4, CheckInTime = DateTime.Now },
                //new Models.ParkedVehicle { TypeOfVehicle = Car, RegNumber = "MAN222", Brand = "Aston Martin", Model = "500L", Color = "Gul", NoOfWheels = 4, CheckInTime = DateTime.Now },
                //new Models.ParkedVehicle { TypeOfVehicle = Car, RegNumber = "WWW333", Brand = "VW", Model = "Golf", Color = "Grön", NoOfWheels = 4, CheckInTime = DateTime.Now },

                //new Models.ParkedVehicle { TypeOfVehicle = Bus, RegNumber = "HIP444", Brand = "Scania", Model = "DC13", Color = "Brun", NoOfWheels = 8, CheckInTime = DateTime.Now },
                //new Models.ParkedVehicle { TypeOfVehicle = Boat, RegNumber = "HOP555", Brand = "Uttern", Model = "U127", Color = "Grå", NoOfWheels = 0, CheckInTime = DateTime.Now },
                //new Models.ParkedVehicle { TypeOfVehicle = Airplane, RegNumber = "SAS123", Brand = "Fokker", Model = "F-28", Color = "Vit", NoOfWheels = 16, CheckInTime = DateTime.Now },

                //new Models.ParkedVehicle { TypeOfVehicle = Car, RegNumber = "FIN753", Brand = "BMW", Model = "F800R", Color = "Svart", NoOfWheels = 4, CheckInTime = DateTime.Now },
                //new Models.ParkedVehicle { TypeOfVehicle = Car, RegNumber = "LOS987", Brand = "FIAT", Model = "500L", Color = "Gul", NoOfWheels = 4, CheckInTime = DateTime.Now },
                //new Models.ParkedVehicle { TypeOfVehicle = Car, RegNumber = "BIL144", Brand = "Volvo", Model = "144", Color = "Vit", NoOfWheels = 4, CheckInTime = DateTime.Now },

                //new Models.ParkedVehicle { TypeOfVehicle = Motorcycle, RegNumber = "DUC565", Brand = "Ducati", Model = "D1200R", Color = "Svart", NoOfWheels = 2, CheckInTime = DateTime.Now },
                //new Models.ParkedVehicle { TypeOfVehicle = Motorcycle, RegNumber = "HUS987", Brand = "Husqvarna", Model = "500cc", Color = "Gul", NoOfWheels = 2, CheckInTime = DateTime.Now },
                //new Models.ParkedVehicle { TypeOfVehicle = Motorcycle, RegNumber = "HON787", Brand = "Honda", Model = "750", Color = "Grön", NoOfWheels = 2, CheckInTime = DateTime.Now },

                //new Models.ParkedVehicle { TypeOfVehicle = Boat, RegNumber = "BÅT003", Brand = "Buster", Model = "DC13", Color = "Vit", NoOfWheels = 0, CheckInTime = DateTime.Now },
                //new Models.ParkedVehicle { TypeOfVehicle = Boat, RegNumber = "ZXY321", Brand = "Princess", Model = "P44", Color = "Blå", NoOfWheels = 0, CheckInTime = DateTime.Now },
            };
            vehicles.ForEach(v => context.Vehicles.AddOrUpdate(v));
            context.SaveChanges();
        }

        //string MapPath(string seedFile)
        //{
        //    if (HttpContext.Current != null)
        //        return HostingEnvironment.MapPath(seedFile);

        //    var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath; //går till mappen bin
        //    var directoryName = Path.GetDirectoryName(absolutePath);
        //    var path = Path.Combine(directoryName, ".." + seedFile.TrimStart('~').Replace('/', '\\'));

        //    return path.Replace(@"\bin\..", "");
        //}
    }
}
