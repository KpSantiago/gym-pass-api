using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class NotFoundRegisterException : ArgumentException
    {
        public NotFoundRegisterException(string message) : base(message: message) { }
    }
}