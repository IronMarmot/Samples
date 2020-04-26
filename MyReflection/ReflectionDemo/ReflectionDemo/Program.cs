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
    class Program
    {
        static void Main(string[] args)
        {
            //重构前，将文件全部显示出来可用
            { 
                ////项目最初,只支持MySQL
                //MySqlHelper mySqlHelper = new MySqlHelper();
                //mySqlHelper.Query();

                ////第二版是，说支持一下SQLServer吧
                ////于是添加一下SQLServerHelper，然后发现，都是数据库操作类，刚才写一个接口IDBHelper吧，比较大家都是要成为架构师的人，这点小封装、优化还是要有的嘛
                //SqlServerHelper sqlServerHelper = new SqlServerHelper();
                //sqlServerHelper.Query();

                ////通过配置文件，来决定使用哪种数据库
                ////通过工厂来封装
                ////string dll = ConfigurationManager.AppSettings["IDBHelperConfig"].Split(',')[0];
                ////string type = ConfigurationManager.AppSettings["IDBHelperConfig"].Split(',')[1];
                ////Assembly assembly = Assembly.Load(dll);
                ////Type type1 = assembly.GetType(type);
                ////object o = Activator.CreateInstance(type1);
                ////IDBHelper dBHelper = o as IDBHelper;
                ////dBHelper.Query();

                //IDBHelper dBHelper = SimpleFactory.CreateInstance();
                //dBHelper.Query();
            }

            //重构后
            {
                IDBHelper dBHelper = SimpleFactory.CreateInstance();
                dBHelper.Query();
            }
            
        }
    }
}
