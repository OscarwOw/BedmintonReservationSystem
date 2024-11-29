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

        public CourtRepository(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }
        public Court GetCourtById(int id)
        {
            var query = @"
                SELECT 
                    c.id, c.name, c.location
                FROM ""Court"" c
                WHERE c.id = @id";

            var parameters = new Dictionary<string, object> { { "@id", id } };
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
            return _databaseAccess.ExecuteQueryToList<Court>(query, parameters);
        }
    }
}
