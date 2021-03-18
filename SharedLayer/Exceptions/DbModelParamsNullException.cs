using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Exceptions
{
    public class DbModelParamsNullException : Exception
    {
        public DbModelParamsNullException()
        {
        }

        public DbModelParamsNullException(string message) : base(message)
        {
        }
    }
}
