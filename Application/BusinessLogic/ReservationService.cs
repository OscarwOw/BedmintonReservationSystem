using Application.Interfaces;
using DataAccess.Interfaces;
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
        private IReservationRepository _reservationRepository;
        private ICourtRepository _courtRepository;
        public ReservationService(IReservationRepository reservationRepository, ICourtRepository courtRepository) 
        {
            _reservationRepository = reservationRepository;
            _courtRepository = courtRepository;
        }
        public List<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetReservations();
        }

        public List<Reservation> GetReservationsByDate(DateTime date)
        {
            return _reservationRepository.GetReservationsByDate(date);
        }

        public Dictionary<int, List<Reservation>> GetReservationsByDateAndLocation(DateTime date, string location)
        {
            List<Reservation> reservations = _reservationRepository.GetReservationsByDate(date);
            List<Court> courts = _courtRepository.GetCourtsByLocation(location);

            Dictionary<int, List<Reservation>> courtsReservations = new Dictionary<int, List<Reservation>>();
            
            foreach (Court court in courts)
            {
                List<Reservation> filteredReservations = new List<Reservation>();
                filteredReservations = reservations.Where(r => r.CourtId == court.Id).ToList();
                courtsReservations[court.Id] = filteredReservations;
            }
            return courtsReservations;
        }

        public List<Reservation> GetReservationsByUser(int UserId)
        {
            return _reservationRepository.GetReservationsByUser(UserId);
        }
    }
}
