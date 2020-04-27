using DB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.MySql
{
    public class MySqlHelper : IDBHelper
    {
        public MySqlHelper()
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

        #region Method
        /// <summary>
        /// 无参方法
        /// </summary>
        public void Show1()
        {
            Console.WriteLine("这里是{0}的Show1", this.GetType());
        }
        /// <summary>
        /// 有参数方法
        /// </summary>
        /// <param name="id"></param>
        public void Show2(int id)
        {

            Console.WriteLine("这里是{0}的Show2", this.GetType());
        }
        /// <summary>
        /// 重载方法之一
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void Show3(int id, string name)
        {
            Console.WriteLine("这里是{0}的Show3", this.GetType());
        }
        /// <summary>
        /// 重载方法之二
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public void Show3(string name, int id)
        {
            Console.WriteLine("这里是{0}的Show3_2", this.GetType());
        }
        /// <summary>
        /// 重载方法之三
        /// </summary>
        /// <param name="id"></param>
        public void Show3(int id)
        {

            Console.WriteLine("这里是{0}的Show3_3", this.GetType());
        }
        /// <summary>
        /// 重载方法之四
        /// </summary>
        /// <param name="name"></param>
        public void Show3(string name)
        {

            Console.WriteLine("这里是{0}的Show3_4", this.GetType());
        }
        /// <summary>
        /// 重载方法之五
        /// </summary>
        public void Show3()
        {

            Console.WriteLine("这里是{0}的Show3_1", this.GetType());
        }
        /// <summary>
        /// 私有方法
        /// </summary>
        /// <param name="name"></param>
        private void Show4(string name)
        {
            Console.WriteLine("这里是{0}的Show4", this.GetType());
        }
        /// <summary>
        /// 静态方法
        /// </summary>
        /// <param name="name"></param>
        public static void Show5(string name)
        {
            Console.WriteLine("这里是{0}的Show5", typeof(MySqlHelper));
        }
        #endregion
    }
}
