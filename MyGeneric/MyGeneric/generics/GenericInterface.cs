using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGeneric.generics
{
    //interface GenericInterface<T, S>
    //{
    //    void Show(T t);
    //    T GetT();

    //    void Sum(T t, S s);
    //}

    interface GenericInterface<T,S>
        where T:class
        where S:struct
    {
        void Show(T t);
        T GetT();

        void Sum(T t, S s);
    }
}
