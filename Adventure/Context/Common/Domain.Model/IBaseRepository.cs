using System;
using System.Collections.Generic;

namespace HRSaga.Adventure.Context.Common.Domain.Model
{
    public interface IBaseRepository<T>
    {
        T GetByID(Guid id);
        List<T> GetMultiple(List<Guid> ids);
        bool Exists(Guid id);
        void Save(T item);
    } 
}