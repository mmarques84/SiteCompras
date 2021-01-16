using System;
using SC.Core.DomainObjects;

namespace SC.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        
    }
}