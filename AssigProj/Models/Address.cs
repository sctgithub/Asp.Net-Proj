using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssigProj.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string HouseNo { get; set; }

        [Required]
        [StringLength(100)]
        public string Line1 { get; set; }
        public string Line2 { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

    }
}