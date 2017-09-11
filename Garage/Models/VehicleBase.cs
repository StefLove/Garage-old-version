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
        public int MemberId { get; set; }

        [Display(Name = "Ägare")]
        //[StringLength(100)]
        //[Required(ErrorMessage = "{0} måste anges!")]
        public string MemberName { get; set; }

        [Display(Name = "Fordonstyp")]
        //[Required(ErrorMessage = "{0} måste anges!")]
        public string VehicleType { get; set; }

        [Display(Name = "Registeringsnummer")]
        //[StringLength(16)]
        //[Required(ErrorMessage = "{0} måste anges!")]
        public string RegNumber { get; set; }

        [Display(Name = "Check-in tid")]
        [Editable(false)]
        [DisplayFormat(DataFormatString = "{0: HH:mm:ss  ddd d MMM}")]
        public DateTime CheckInTime { get; set; }
    }
}