using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICourtService
    {
        public List<Court> GetCourts();
        public List<Court> GetCourtsByLocation(string location);
    }
}
