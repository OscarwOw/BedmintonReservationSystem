using Application.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic
{
    public class ReservationService : IReservationService
    {
        private ReservationRepository _reservationRepository;
        public ReservationService(ReservationRepository reservationRepository) 
        {
            _reservationRepository = reservationRepository;
        }
        public List<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetReservations();
        }

        public List<Reservation> GetReservationsByDate(DateTime date)
        {
            return _reservationRepository.GetReservationsByDate(date);
        }

        public List<Reservation> GetReservationsByUser(int UserId)
        {
            return _reservationRepository.GetReservationsByUser(UserId);
        }
    }
}
