using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _15_ExceptionDemo.Exceptions
{
    public class MyException:Exception,IKnowException
    {
        public MyException(string message,int ErrorCode,params object[] data):base(message)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorData = data;
        }

        public int ErrorCode { get; private set; }
        public object[] ErrorData { get; private set; }
    }
}
