using System.Collections.Generic;

namespace CityAppREST.Models
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        T Create(T toCreate);
        T Update(T toUpdate);
        T Delete(int id);
        void SaveChanges();
    }
}
