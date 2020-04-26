using DB.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionDemo
{
    public static class SimpleFactory
    {
        //通过配置文件，来决定使用哪种数据库
        static string dllName = ConfigurationManager.AppSettings["IDBHelperConfig"].Split(',')[0];
        static string typeName = ConfigurationManager.AppSettings["IDBHelperConfig"].Split(',')[1];

        public static IDBHelper CreateInstance()
        {
            Assembly assembly = Assembly.Load(dllName);
            Type type1 = assembly.GetType(typeName);
            object o = Activator.CreateInstance(type1);
            IDBHelper dBHelper = o as IDBHelper;
            return dBHelper;
        }
        
    }
}
