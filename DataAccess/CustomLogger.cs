using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CustomLogger : ICustomLogger
    {
        public CustomLogger()
        {
            
        }

        public void Error(string message)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(DateTime.Now.ToString()).Append(" | Error: ").Append(message);
            Log(stringBuilder.ToString());
        }

        public void Info(string message)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(DateTime.Now.ToString()).Append(" | Info: ").Append(message);
            Log(stringBuilder.ToString());
        }

        public void Warn(string message)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(DateTime.Now.ToString()).Append(" | Warning: ").Append(message);
            Log(stringBuilder.ToString());
        }

        private void Log(string message)
        {
            using (StreamWriter streamWriter = new("log.txt", append: true))
            {
                streamWriter.WriteLine(message);
            }
        }
    }
}
