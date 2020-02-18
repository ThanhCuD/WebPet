using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPet.Models.SQLRepository
{
    public interface IXRepository<T> where T : class
    {
        T GetById(int Id);
        IEnumerable<T> GetAll();
        T Add(T obj);
        T Delete(int id);
        T Update(T obj);
    }
}
