using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetReservations();
        Reservation? GetReservationById(int id);
        List<Reservation> GetReservationsByCourt(int courtId);
        List<Reservation> GetReservationsByUser(int userId);
        List<Reservation> GetReservationsByDate(DateTime dateTime);
        List<Reservation> GetReservationsByUserByDate(int userId, DateTime dateTime);
        public bool AddReservation(Reservation reservation);
        public bool DeleteReservation(int reservationId);
    }
}
