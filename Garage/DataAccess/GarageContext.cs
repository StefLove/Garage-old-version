using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Garage.DataAccess
{
    public class GarageContext : DbContext
    {
        public GarageContext() : base("Garage")
        {

        }
        public DbSet<Models.Vehicle> Vehicles { get; set; }
        public DbSet<Models.Member> Members { get; set; }
        public DbSet<Models.VehicleType> VehicleTypes { get; set; }
    }
}