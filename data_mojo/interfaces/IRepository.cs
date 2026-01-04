using data_mojo.models;
using data_mojo.dtos;

namespace data_mojo.interfaces

{
    public interface IRepository<T, TAdd, TUpdate>
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task<T?> GetByName(string name);
        Task<T> Add(TAdd dto);
        Task<T?> Upadte(TUpdate dto);
        Task<bool> Delete(int id);
    }
}