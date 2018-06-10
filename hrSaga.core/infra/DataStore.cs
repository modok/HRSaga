using System;
using System.Collections.Generic;
using System.Linq;

namespace hrSaga.core.infra
{
    public class DataStore
    {
        readonly Dictionary<Type, Dictionary<Guid, BaseEntity>> _registry
            = new Dictionary<Type, Dictionary<Guid, BaseEntity>>();

        public void Insert(BaseEntity entity)
        {
            var entityType = entity.GetType();
            if (!_registry.ContainsKey(entityType))
            {
                _registry.Add(entityType, new Dictionary<Guid, BaseEntity>());
            }

            _registry[entityType].Add(entity.Id, entity);
        }

        public E GetFirst<E>()
            where E : BaseEntity
        {
            var entityType = typeof(E);
            return _registry[entityType].First().Value as E;
        }

        public E Get<E>(Guid id)
            where E : BaseEntity
        {
            var entityType = typeof(E);
            return _registry[entityType][id] as E;
        }

        public void Update(BaseEntity entity)
        {
            var entityType = entity.GetType();
            _registry[entityType][entity.Id] = entity;
        }

        public void Delete<E>(Guid id)
            where E : BaseEntity
        {
            var entityType = typeof(E);
            _registry[entityType].Remove(id);
        }
    }
}
