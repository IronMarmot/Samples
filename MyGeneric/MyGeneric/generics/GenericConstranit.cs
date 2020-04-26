using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * five-constranit type
 * Base class constranit
 * interface constranit
 * class constranit
 * struct constranit
 * new ()
 */
namespace MyGeneric.generics
{
    class GenericConstranit
    {
        public void Show<T>() 
            where T:Person
        {

        }

        public void Show1<T>()
            where T:IActions
        {

        }

        public void Show<T,S>()
            where T:class
            where S:struct
        {

        }

        public void Show2<S>()
            where S:new()
        {

        }
    }
}
