using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGeneric.generics
{
    class GenericClass<T>
    {
        public T GetT()
        {
            throw new NotImplementedException();
        }

        public void Show(T t)
        {
            throw new NotImplementedException();
        }
    }

    class GenericClass<T, S>
    {
        public T GetT()
        {
            throw new NotImplementedException();
        }

        public void Show(T t)
        {
            throw new NotImplementedException();
        }

        public void Sum(T t, S s)
        {
            throw new NotImplementedException();
        }
    }

    //class implement the generic interface,the type params is necessary.
    class GenericClass1<T,S>: GenericInterface<T, S>
        where T:class
        where S:struct
     //class GenericClass1<T, S> : GenericInterface<T, S>
    {
        public T GetT()
        {
            throw new NotImplementedException();
        }

        public void Show(T t)
        {
            throw new NotImplementedException();
        }

        public void Sum(T t, S s)
        {
            throw new NotImplementedException();
        }
    }

    //class Inherited base class,the type params is necessary.
    class GeneriClass2<T,S> : GenericClass<T, S>
    {

    }
}
