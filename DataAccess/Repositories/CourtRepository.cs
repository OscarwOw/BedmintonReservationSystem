using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CourtRepository : ICourtRepository
    {
        private readonly IDatabaseAccess _databaseAccess;
        private readonly ICustomLogger _customLogger;

        public CourtRepository(IDatabaseAccess databaseAccess, ICustomLogger customLogger)
        {
            _databaseAccess = databaseAccess;
            _customLogger = customLogger;
        }
        public Court GetCourtById(int id)
        {
            var query = @"
                SELECT 
                    c.id, c.name, c.location
                FROM ""Court"" c
                WHERE c.id = @id";

            var parameters = new Dictionary<string, object> { { "@id", id } };
            _customLogger.Info("Executing GetCourtById query");
            return _databaseAccess.ExecuteQueryToList<Court>(query, parameters).FirstOrDefault();
        }

        public Court GetCourtByName(string name)
        {
            var query = @"
                SELECT 
                    c.id, c.name, c.location
                FROM ""Court"" c
                WHERE c.name = @name";

            var parameters = new Dictionary<string, object> { { "@name", name } };
            _customLogger.Info("Executing GetCourtByName query");
            return _databaseAccess.ExecuteQueryToList<Court>(query, parameters).FirstOrDefault();
        }

        public List<Court> GetCourts()
        {
            var query = @"
                SELECT 
                    c.id, c.name, c.location
                FROM ""Court"" c";

            return _databaseAccess.ExecuteQueryToList<Court>(query);
        }

        public List<Court> GetCourtsByLocation(string location)
        {
            var query = @"
                SELECT 
                    c.id, c.name, c.location
                FROM ""Court"" c
                WHERE c.location = @location";

            var parameters = new Dictionary<string, object> { { "@location", location } };
            _customLogger.Info("Executing GetCourtsByLocation query");
            return _databaseAccess.ExecuteQueryToList<Court>(query, parameters);
        }
    }
}
