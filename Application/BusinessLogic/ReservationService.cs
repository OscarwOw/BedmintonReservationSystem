using Application.Interfaces;
using DataAccess.Entities;
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
        private ILoginCacheService _loginCacheService;
        public ReservationService(IReservationRepository reservationRepository, ICourtRepository courtRepository, ILoginCacheService loginCacheService) 
        {
            _reservationRepository = reservationRepository;
            _courtRepository = courtRepository;
            _loginCacheService = loginCacheService;
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

        public Reservation GetReservationById(int ReservationId)
        {
            return _reservationRepository.GetReservationById(ReservationId);
        }

        public bool DeleteReservation(string token, int reservationId)
        {
            Reservation reservation = _reservationRepository.GetReservationById(reservationId);
            if(reservation is null){
                return false;
            }
            int tokenUserId = Int32.Parse(token.Split('t')[0]); 
            if(reservation.UserId != tokenUserId)
            {
                return false;
            }

            (DateTime, DateTime, string)? userinfo = _loginCacheService.GetUserInfo(tokenUserId);
            if (userinfo.HasValue)
            {
                if (userinfo.Value.Item3 == token)
                {
                    _reservationRepository.DeleteReservation(reservationId);
                    return true;
                }
            }
            return false;           
        }

        public bool AddReservation(string token, Reservation reservation)
        {
            if(reservation == null)
            {
                return false;
            }
            var tokenParts = token.Split('t');
            if (tokenParts.Length < 1 || !Int32.TryParse(tokenParts[0], out int UserId))
            {
                return false; 
            }
            (DateTime, DateTime, string)? userinfo = _loginCacheService.GetUserInfo(UserId);
            if (userinfo.HasValue)
            {
                if (userinfo.Value.Item3 == token)
                {
                    reservation.UserId = UserId;
                    if(checkReservationValues(reservation))
                    {
                        _reservationRepository.AddReservation(reservation);
                        return true;
                    }

                    
                }
            }
            return false;
        }


        private bool checkReservationValues(Reservation reservation)
        {
            if (reservation.StartTime.Minute != 0 || reservation.StartTime.Second != 0)
            {
                return false; 
            }

            Court court = _courtRepository.GetCourtById(reservation.CourtId);
            if(court is null)
            {
                return false;
            }

            return true;
        }
    }
}
