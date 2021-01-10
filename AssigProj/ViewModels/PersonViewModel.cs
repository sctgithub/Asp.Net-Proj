using AssigProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssigProj.ViewModels
{
    public class PersonViewModel
    {
        public Person person { get; set; }
        public List<Address> addresses { get; set; }
    }
}