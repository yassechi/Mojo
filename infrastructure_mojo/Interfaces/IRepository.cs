using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace infrastructure_mojo.Interfaces

{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task<T> Add(T dto);
        Task<T?> Upadte(T dto);
        Task<bool> Delete(int id);
    }
}