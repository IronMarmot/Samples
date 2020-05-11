using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace _18_FileProviderDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileProvider fileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);
            var contents = fileProvider.GetDirectoryContents("/");

            foreach (var item in contents)
            {
                Console.WriteLine(item.Name);
                //Stream stream=item.CreateReadStream();
            }
            Console.WriteLine("===================");

            //这种方式可正常包含嵌入资源
            //IFileProvider fileProvider1 = new EmbeddedFileProvider(typeof(Program).Assembly, "_18_FileProviderDemo");

            //在csproj中不能指定rootNamespces，才能正常包含嵌入资源
            IFileProvider fileProvider1 = new EmbeddedFileProvider(typeof(Program).Assembly);

            var html = fileProvider1.GetFileInfo("a.html");
            Console.WriteLine(html);
            Console.WriteLine("======================");

            IFileProvider fileProvider2 = new CompositeFileProvider(fileProvider, fileProvider1);

            var contents1 = fileProvider2.GetDirectoryContents("/");

            foreach (var item in contents1)
            {
                Console.WriteLine(item.Name);
            }
            Console.ReadKey();

        }
    }
}
