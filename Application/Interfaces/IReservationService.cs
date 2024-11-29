using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReservationService
    {
        public List<Reservation> GetAllReservations();
        public List<Reservation> GetReservationsByDate(DateTime date);
        public List<Reservation> GetReservationsByUser(int UserId);
    }
}
