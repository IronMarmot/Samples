using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _02_DependencyInjection.services
{
    public interface IGenericServie<T>
    {

    }

    public class GenericService<T>:IGenericServie<T>
    {
        public T Data { get; set; }

        public GenericService(T Data)
        {
            this.Data = Data;
        }
    }
}
