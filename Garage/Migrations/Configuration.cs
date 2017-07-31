namespace Garage.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Garage.Models;
    using static Models.TypeOfVehicle;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage.DataAccess.GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Garage.DataAccess.GarageContext context)
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
                new Member { Name = "Carl", Membershipnumber = "4652", Phonenumber = "070 558 666", Address = "City" },
                new Member { Name = "Flin", Membershipnumber = "3267", Phonenumber = "070 585 666", Address = "City" },
                new Member { Name = "Alex", Membershipnumber = "1287", Phonenumber = "070 955 666", Address = "City" },
                new Member { Name = "Gerd", Membershipnumber = "8833", Phonenumber = "070 595 666", Address = "City" },
                new Member { Name = "Jana", Membershipnumber = "5566", Phonenumber = "070 559 666", Address = "City" },
                new Member { Name = "Kent", Membershipnumber = "5432", Phonenumber = "070 555 966", Address = "City" },
                new Member { Name = "Anna", Membershipnumber = "4567", Phonenumber = "070 555 696", Address = "City" },
                new Member { Name = "Bert", Membershipnumber = "1234", Phonenumber = "070 555 669", Address = "Alby" }
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
                new Vehicle { VehicleTypeId = 13, RegNumber = "QTY555", Brand = "Boeing", Model = "747", Color = "Vit", NoOfWheels = 20, CheckInTime = DateTime.Now, MemberId = 8 }
            };
            vehicles.ForEach(v => context.Vehicles.AddOrUpdate(v));
            context.SaveChanges();
        }
    }
}
