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
        public Dictionary<int, List<Reservation>> GetReservationsByDateAndLocation(DateTime date, string location);
        public Reservation GetReservationById(int ReservationId);
        public bool DeleteReservation(string token, int reservationId);
        public bool AddReservation(string token, Reservation reservation);
    }
}
