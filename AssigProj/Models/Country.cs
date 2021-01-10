using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssigProj.Models
{
    public class Country
    {
        public int CountryId { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

        public virtual List<City> Cities { get; set; }

    }
}