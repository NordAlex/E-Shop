using EShop.Carting.Domain.Common.Interfaces;

namespace EShop.Carting.Domain.Common
{
    public abstract class BaseEntity: IEntity
    {
        public int Id { get; set; }
    }
}
