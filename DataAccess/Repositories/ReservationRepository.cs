using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IDatabaseAccess _databaseAccess;
        private readonly ICustomLogger _customLogger;
        public ReservationRepository(IDatabaseAccess databaseAccess, ICustomLogger customLogger)
        {
            _databaseAccess = databaseAccess;
            _customLogger = customLogger;
        }

        public Reservation? GetReservationById(int id)
        {
            var query = @"
                SELECT 
                    r.id, r.userid, r.courtid, r.starttime,  
                    u.id AS UserId, u.name AS UserName, u.password AS UserPassword,
                    c.id AS CourtId, c.name AS CourtName, c.location AS CourtLocation
                FROM ""Reservation"" r
                INNER JOIN ""User"" u ON r.userid = u.id
                INNER JOIN ""Court"" c ON r.courtid = c.id
                WHERE r.id = @id";

            var parameters = new Dictionary<string, object> { { "@id", id } };
            _customLogger.Info("Executing GetReservationById query");
            return _databaseAccess.ExecuteQueryToList<Reservation>(query, parameters).FirstOrDefault();
        }

        public List<Reservation> GetReservationsByCourt(int courtId)
        {
            var query = @"
                SELECT 
                    r.id, r.userid, r.courtid, r.starttime
                FROM ""Reservation"" r
                WHERE r.courtid = @courtid";

            var parameters = new Dictionary<string, object> { { "@courtid", courtId } };
            _customLogger.Info("Executing GetReservationsByCourt query");
            return _databaseAccess.ExecuteQueryToList<Reservation>(query, parameters);
        }

        public List<Reservation> GetReservationsByDate(DateTime dateTime)
        {
            var query = @"
                SELECT 
                    r.id, r.userid, r.courtid, r.starttime
                FROM ""Reservation"" r
                WHERE DATE(r.starttime) = @date";

            var parameters = new Dictionary<string, object> { { "@date", dateTime.Date } };
            _customLogger.Info("Executing GetReservationsByDate query");
            return _databaseAccess.ExecuteQueryToList<Reservation>(query, parameters);
        }

        public List<Reservation> GetReservationsByUser(int userId)
        {
            var query = @"
                SELECT 
                    r.id, r.userid, r.courtid, r.starttime
                FROM ""Reservation"" r
                WHERE r.userid = @userid";

            var parameters = new Dictionary<string, object> { { "@userid", userId } };
            _customLogger.Info("Executing GetReservationsByUser query");
            return _databaseAccess.ExecuteQueryToList<Reservation>(query, parameters);
        }

        public List<Reservation> GetReservationsByUserByDate(int userId, DateTime dateTime)
        {
            var query = @"
                SELECT 
                    r.id, r.userid, r.courtid, r.starttime
                FROM ""Reservation"" r
                WHERE r.userid = @userid AND DATE(r.starttime) = @date";

            var parameters = new Dictionary<string, object>
            {
                { "@userid", userId },
                { "@date", dateTime.Date }
            };
            _customLogger.Info("Executing GetReservationsByUserByDate query");
            return _databaseAccess.ExecuteQueryToList<Reservation>(query, parameters);
        }

        public List<Reservation> GetReservations()
        {
            var query = @"
                SELECT 
                    r.id, r.userid, r.courtid, r.starttime
                FROM ""Reservation"" r";

            _customLogger.Info("Executing GetReservations query");
            return _databaseAccess.ExecuteQueryToList<Reservation>(query);
        }

        public bool AddReservation(Reservation reservation)
        {
            var query = @" INSERT INTO ""Reservation"" (""userid"", ""courtid"", ""starttime"") VALUES (@userid, @courtid, @starttime)";

            var parameters = new Dictionary<string, object>
            {
                { "@userid", reservation.UserId },
                { "@courtid", reservation.CourtId },
                { "@starttime", reservation.StartTime }
            };

            var rowsAffected = _databaseAccess.ExecuteNonQuery(query, parameters);
            _customLogger.Info("Executing AddReservation query");
            return rowsAffected > 0; 
        }


        public bool DeleteReservation(int reservationId)
        {
            var query = @" DELETE FROM ""Reservation""  WHERE ""id"" = @id";

            var parameters = new Dictionary<string, object>
            {
                { "@id", reservationId }
            };

            var rowsAffected = _databaseAccess.ExecuteNonQuery(query, parameters);
            _customLogger.Info("Executing DeleteReservation query");
            return rowsAffected > 0; 
        }



    }
}
