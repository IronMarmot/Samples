using MyGeneric.generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            List<GenericClass<Person>> genericClasses = new List<GenericClass<Person>>();

            //List<GenericClass1<Person, Animal>> genericClass1s = new List<GenericClass1<Person, Animal>>();

            GenericConstranit genericConstranit = new GenericConstranit();

            //where T:Person
            genericConstranit.Show<Person>();
            //genericConstranit.Show<Animal>();

            //where T：IActions
            genericConstranit.Show1<Animal>();
            //genericConstranit.Show1<Person>();

            //where T:class
            //where S:struct
            genericConstranit.Show<Person, int>();

            //where T：new ()
            genericConstranit.Show2<Person>();


            //class implement interface which has constranit where T:class  where S:struct
            List<GenericClass1<Person, int>> genericClass1s = new List<GenericClass1<Person, int>>();
        }
    }
}
