using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Exceptions
{
    public class DbModelParamFormatException : Exception
    {
        public DbModelParamFormatException()
        {
        }

        public DbModelParamFormatException(string message) : base(message)
        {
        }
    }
}
