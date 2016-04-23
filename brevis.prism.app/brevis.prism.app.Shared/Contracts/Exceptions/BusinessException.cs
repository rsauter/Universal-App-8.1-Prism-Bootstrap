using System;
using System.Collections.Generic;
using System.Text;

namespace brevis.prism.app.Contracts.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message)
        {
        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
