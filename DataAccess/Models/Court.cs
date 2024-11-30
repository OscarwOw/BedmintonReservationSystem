﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Court
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        //direct references
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}