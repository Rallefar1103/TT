using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistrationApplication.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string s) : base(s)
        {

        }
    }
}
