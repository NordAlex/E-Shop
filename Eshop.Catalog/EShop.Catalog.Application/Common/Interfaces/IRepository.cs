using EShop.Catalog.Domain.Common.Interfaces;

namespace EShop.Catalog.Application.Common.Interfaces
{
    public interface IRepository<T>
        where T : class, IEntity
    {
        public int Insert(T item);
        public bool Update(T item);
        public bool Delete(T item);
    }
}
