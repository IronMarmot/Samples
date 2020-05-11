using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _15_ExceptionDemo.Exceptions
{
    public interface IKnowException
    {
        public string Message { get; }
        public int ErrorCode { get; }
        public object[] ErrorData { get; }
    }
}
