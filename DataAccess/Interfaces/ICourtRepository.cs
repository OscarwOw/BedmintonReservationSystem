using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ICourtRepository
    {
        public List<Court> GetCourts();
        public List<Court> GetCourtsByLocation(string location);
        public Court GetCourtById(int Id);
        public Court GetCourtByName(string Name);
    }
}
