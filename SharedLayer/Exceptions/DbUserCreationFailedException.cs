using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Exceptions
{
    public class DbUserCreationFailedException : Exception
    {
        public DbUserCreationFailedException()
        {
        }

        public DbUserCreationFailedException(string message) : base(message)
        {
        }
    }
}
