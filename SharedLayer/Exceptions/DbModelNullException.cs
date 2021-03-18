using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Exceptions
{
    public class DbModelNullException : Exception
    {
        public DbModelNullException()
        {
        }

        public DbModelNullException(string message) : base(message)
        {
        }
    }
}
