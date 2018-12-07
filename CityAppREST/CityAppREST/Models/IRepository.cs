using System.Collections.Generic;

namespace CityAppREST.Models
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Create(T toCreate);
        void Update(T toUpdate);
        void Delete(int id);
        void SaveChanges();
    }
}
