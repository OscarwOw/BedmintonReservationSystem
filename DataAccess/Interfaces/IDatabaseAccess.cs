using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IDatabaseAccess
    {
        DataTable ExecuteQuery(string query, IDictionary<string, object> parameters = null);
        List<T> ExecuteQueryToList<T>(string query, IDictionary<string, object> parameters = null) where T : class, new();
    }
}

