using System;
using System.Collections.Generic;

namespace KafeAdisyon.Business.Abstracts
{
    public interface IRepository<T> where T : CafeBase
    {
        CafeContext Context { get; set; }
        T Get(Guid id);
        List<T> GetAll(Func<T, bool> predicate = null);
        void Add(T item);
        void Remove(T item);
        void Update();
    }
}