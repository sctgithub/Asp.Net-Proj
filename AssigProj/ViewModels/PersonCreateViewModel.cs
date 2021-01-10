using AssigProj.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssigProj.ViewModels
{
    public class PersonCreateViewModel
    {       
        public Person person { get; set; }

        public Address address { get; set; }
        
        [Display(Name = "Country")]
        public List<Country> countries { get; set; }

        [Display(Name = "City")]
        public List<City> cities { get; set; }
    }
}