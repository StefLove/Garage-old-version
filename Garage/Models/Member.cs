using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

namespace Garage.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Display(Name = "Medlemsnamn")]
        public string Name { get; set; }

        [Display(Name = "Medlemsnummer")]
        public string Membershipnumber { get; set; }

        [Display(Name = "Telefonnummer")]
        public string Phonenumber { get; set; }

        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Display(Name = "Fordon")]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}