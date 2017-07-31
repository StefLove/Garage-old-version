using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

namespace Garage.Models
{
    public class VehicleBase
    {
        public int Id { get; set; }

        [Display(Name = "Ägare")]
        //[Required(ErrorMessage = "{0} måste anges!")]
        //[StringLength(100)]
        public string MemberName { get; set; }

        [Display(Name = "Fordonstyp")]
        //[Required(ErrorMessage = "{0} måste anges!")]
        public string VehicleType { get; set; }

        [Display(Name = "Registeringsnummer")]
        //[Required(ErrorMessage = "{0} måste anges!")]
        //[StringLength(16)]
        public string RegNumber { get; set; }

        [Display(Name = "Check-in tid")]
        [Editable(false)]
        [DisplayFormat(DataFormatString = "{0: HH:mm:ss  ddd d MMM}")]
        public DateTime CheckInTime { get; set; }
    }
}