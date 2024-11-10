using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDApplication.Shared.Exceptions
{
    public class ConflictInfosExcpetion : ArgumentException
    {
        public ConflictInfosExcpetion(string message) : base(message: message) { }
    }
}