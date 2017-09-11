using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

namespace Garage.Models
{
    public class Vehicle
    {
        public Vehicle()
        {
            CheckInTime = DateTime.Now;
        }

        public int Id { get; set; }

        [Display(Name = "Ägare")]
        public int MemberId { get; set; }
        [Display(Name = "Ägare")]
        //[StringLength(100)]
        //[Required(ErrorMessage = "{0} måste anges!")]
        public virtual Member Member { get; set; }

        [Display(Name = "Fordonstyp")]
        //[Required(ErrorMessage = "{0} måste anges!")]
        public virtual VehicleType VehicleType { get; set; }

        public int VehicleTypeId { get; set; }

        [Display(Name = "Registeringsnummer")]
        [Required(ErrorMessage = "{0} måste anges!")]
        [RegularExpression(@"\D{1,3}\w+", ErrorMessage = "Ange ett giltigt {0}")]
        [MaxLength(16)] //[StringLength(16)]
        public string RegNumber { get; set; }

        [Display(Name = "Färg")]
        [MaxLength(30)] //[StringLength(30)]
        public string Color { get; set; }

        [Display(Name = "Antal hjul")]
        [Range(0, 20)]
        public int NoOfWheels { get; set; }

        [Display(Name = "Märke")]
        [MaxLength(30)] //[StringLength(30)]
        public string Brand { get; set; }

        [Display(Name = "Modell")]
        [MaxLength(30)] //[StringLength(30)]
        public string Model { get; set; }

        [Display(Name = "Check-in tid")]
        [Editable(false)]
        [DisplayFormat(DataFormatString = "{0: HH:mm:ss  ddd d MMM}")]
        public DateTime CheckInTime { get; set; }
    }
}