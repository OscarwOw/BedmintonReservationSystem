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
        public ReservationRepository(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
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
            return _databaseAccess.ExecuteQueryToList<Reservation>(query, parameters);
        }

        public List<Reservation> GetReservations()
        {
            var query = @"
                SELECT 
                    r.id, r.userid, r.courtid, r.starttime
                FROM ""Reservation"" r";

            return _databaseAccess.ExecuteQueryToList<Reservation>(query);
        }

        public bool AddReservation(Reservation reservation)
        {
            var query = @" INSERT INTO ""Reservation"" (""UserId"", ""CourtId"", ""StartTime"") VALUES (@UserId, @CourtId, @StartTime)";

            var parameters = new Dictionary<string, object>
            {
                { "@UserId", reservation.UserId },
                { "@CourtId", reservation.CourtId },
                { "@StartTime", reservation.StartTime }
            };

            var rowsAffected = _databaseAccess.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0; 
        }


        public bool DeleteReservation(int reservationId)
        {
            var query = @" DELETE FROM ""Reservation""  WHERE ""Id"" = @Id";

            var parameters = new Dictionary<string, object>
            {
                { "@Id", reservationId }
            };

            var rowsAffected = _databaseAccess.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0; 
        }



    }
}
