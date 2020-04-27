using DB.MySql;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //{
                //	//Load：通过程序集命名空间
                //	Assembly assembly = Assembly.Load("DB.MySql");

                //	//LoadFile：通过程序集对应的完整物理路径
                //	Assembly assembly1 = Assembly.LoadFile(@"E:\StydyDemos\Samples\MyReflection\MyReflection\DB.MySql\bin\Debug\DB.MySql.dll");

                //	//LoadFrom：通过程序集的文件名或路径加载
                //	//1.文件名
                //	Assembly assembly2 = Assembly.LoadFrom("DB.Mysql.dll");
                //	//2.完整物理路径
                //	Assembly assembly3 = Assembly.LoadFrom(@"E:\StydyDemos\Samples\MyReflection\MyReflection\DB.MySql\bin\Debug\DB.MySql.dll");
                //}
                //{
                //	Assembly assembly = Assembly.Load("DB.MySql");
                //	//获取程序集下全部类型，并打印其名称
                //	Type[] types=assembly.GetTypes();
                //	types.ToList().ForEach(o => Console.WriteLine(o.Name));

                //	//获取assembly的Type
                //	Type type1 = assembly.GetType();

                //	//获取指定类名称的类型
                //	Type type2 = assembly.GetType("DB.MySql.MySqlHelper");
                //}
                //{
                //	Assembly assembly = Assembly.Load("DB.MySql");

                //	Type type = assembly.GetType("DB.MySql.MySqlHelper");

                //	//创建对象
                //	object o = assembly.CreateInstance("DB.MySql.MySqlHelper");
                //	//类型转换
                //	MySqlHelper mySqlHelper = (MySqlHelper)o;
                //}
                {
                    Assembly assembly = Assembly.Load("DB.MySql");
                    Type type = assembly.GetType("DB.MySql.MySqlHelper");
                    object o = assembly.CreateInstance("DB.MySql.MySqlHelper");
                    //============无参方法=============
                    ////不指定参数类型
                    //MethodInfo methodInfo = type.GetMethod("Show1");
                    ////指定参数类型
                    ////MethodInfo methodInfo = type.GetMethod("Show1",new Type[] { });
                    //methodInfo.Invoke(o,null);

                    ////============有参方法=============
                    //MethodInfo methodInfo = type.GetMethod("Show2");
                    ////MethodInfo methodInfo = type.GetMethod("Show2",new Type[] { typeof(int) };
                    //methodInfo.Invoke(o, new object[] { 123 });

                    //============重载方法=============
                    //{//无参
                    //    MethodInfo method = type.GetMethod("Show3");
                    //    method.Invoke(o, null);
                    //}
                    //{//一个参数
                    //    MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(int) });
                    //    method.Invoke(o, new object[] { 123 });
                    //}
                    //{//一个参数
                    //    MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(string) });
                    //    method.Invoke(o, new object[] { "画鸡蛋的不止达芬奇" });
                    //}
                    //{//2个参数
                    //    MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(int), typeof(string) });
                    //    method.Invoke(o, new object[] { 123, "画鸡蛋的不止达芬奇" });
                    //}
                    //{//2个参数
                    //    MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(string), typeof(int) });
                    //    method.Invoke(o, new object[] { "画鸡蛋的不止达芬奇", 123 });
                    //}

                    ////============静态方法=============
                    //{
                    //    MethodInfo methodInfo = type.GetMethod("Show5");
                    //    methodInfo.Invoke(null, new object[]{ "画鸡蛋的不止达芬奇" });
                    //    methodInfo.Invoke(o, new object[]{ "画鸡蛋的不止达芬奇" });
                    //}

                    //============私有方法=============
                    //{
                    //    MethodInfo methodInfo = type.GetMethod("Show4",BindingFlags.Instance|BindingFlags.NonPublic);
                    //    methodInfo.Invoke(o, new object[] { "画鸡蛋的不止达芬奇" });
                    //}

                    //============属性和字段=============
                    {
                        Type type1 = typeof(Person);
                        object oPerson = Activator.CreateInstance(type);

                        PropertyInfo info=type1.GetProperty("Name");
                        foreach (var prop in type1.GetProperties())
                        {
                            Console.WriteLine($"{type.Name}.{prop.Name}={prop.GetValue(oPerson)}");
                            if (prop.Name.Equals("Id"))
                            {
                                prop.SetValue(oPerson, 123);
                            }
                            else if (prop.Name.Equals("Name"))
                            {
                                prop.SetValue(oPerson, "画鸡蛋的不止达芬奇");
                            }
                            Console.WriteLine($"{type.Name}.{prop.Name}={prop.GetValue(oPerson)}");
                        }

                        FieldInfo fieldInfo= type1.GetField("Description");
                        foreach (var field in type1.GetFields())
                        {
                            Console.WriteLine($"{type.Name}.{field.Name}={field.GetValue(oPerson)}");
                            if (field.Name.Equals("Description"))
                            {
                                field.SetValue(oPerson, "画鸡蛋的不止达芬奇");
                            }
                            Console.WriteLine($"{type.Name}.{field.Name}={field.GetValue(oPerson)}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
