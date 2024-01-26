using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Generics
{
    public interface IGenerics<T> where T : class //T é qualquer classe
    {
        Task Add(T objeto);
        Task Uppdate(T objeto);
        Task Delete(T objeto);
        Task<T> GetEntityById(int id);
        Task<List<T>> List();
    }
}
