using EShop.Catalog.Domain.Common.Interfaces;

namespace EShop.Catalog.Domain.Common
{
    public abstract class BaseEntity: IEntity
    {
        public int Id { get; set; }
    }
}
