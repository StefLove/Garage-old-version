using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Garage.Models
{
    public enum TypeOfVehicle
    {
        [Display(Name = "Bil")]
        Car,
        [Display(Name = "Buss")]
        Bus,
        [Display(Name = "Båt")]
        Boat,
        [Display(Name = "Flygplan")]
        Airplane,
        [Display(Name = "Motorcykel")]
        Motorcycle
    }

    public class VehicleType
    {
        public int Id { get; set; }

        [Display(Name = "Fordonstyp")]
        [EnumDataType(typeof(TypeOfVehicle))]
        public TypeOfVehicle TypeOfVehicle { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}