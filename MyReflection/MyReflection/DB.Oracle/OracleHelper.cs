using DB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Oracle
{
    class OracleHelper:IDBHelper
    {
        public OracleHelper()
        {
            Console.WriteLine("{0}构造函数", this.GetType().Name);
        }
        public void Query()
        {
            Console.WriteLine("{0}.Query",this.GetType().Name);
        }

        public void Insert()
        {
            Console.WriteLine("{0}.Insert", this.GetType().Name);
        }
    }
}
