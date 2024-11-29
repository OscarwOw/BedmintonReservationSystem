using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourtId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        //direct references
        public User? User { get; set; }
        public Court? Court { get; set; }
    }
}
