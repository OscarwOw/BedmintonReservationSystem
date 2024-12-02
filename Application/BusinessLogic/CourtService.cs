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
        private readonly ICustomLogger _customLogger;
        public CourtService(ICourtRepository courtRepository, ICustomLogger customLogger)
        {
            _courtRepository = courtRepository;
            _customLogger = customLogger;
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
