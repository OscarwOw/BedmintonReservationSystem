using Application.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic
{
    public class CourtService : ICourtService
    {
        private readonly ICourtRepository _courtRepository;
        public CourtService(ICourtRepository courtRepository)
        {
            _courtRepository = courtRepository;
        }
        public List<Court> GetCourts()
        {
            return _courtRepository.GetCourts();
        }

        public List<Court> GetCourtsByLocation(string location)
        {
            return _courtRepository.GetCourtsByLocation(location);
        }
    }
}
