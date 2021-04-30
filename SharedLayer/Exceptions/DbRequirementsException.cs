using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Exceptions
{
    public class DbRequirementsException : Exception
    {
        public DbRequirementsException()
        {
        }

        public DbRequirementsException(string message) : base(message)
        {
        }
    }
}
