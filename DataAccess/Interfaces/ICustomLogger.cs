using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ICustomLogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
    }
}
