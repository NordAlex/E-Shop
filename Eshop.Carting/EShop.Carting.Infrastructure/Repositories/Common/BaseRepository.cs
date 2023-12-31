﻿using EShop.Carting.Application.Common.Interfaces;
using EShop.Carting.Domain.Common.Interfaces;
using LiteDB;

namespace EShop.Carting.Infrastructure.Repositories.Common
{
    public abstract class BaseRepository<T> : LiteRepository, IRepository<T>
        where T : class, IEntity
    {
        protected BaseRepository(ILiteDatabase database) : base(database)
        {
        }

        public int Insert(T item) => Insert<T>(item, null);
        public bool Update(T item) => Update(item, null);
        public bool Delete(T item) => Delete<T>(new BsonValue(item.Id), null);
        public int Update(IEnumerable<T> items) => Update(items, null);
    }
}
