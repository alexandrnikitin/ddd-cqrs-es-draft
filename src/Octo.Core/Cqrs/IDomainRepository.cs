using System;

namespace Octo.Core.Cqrs
{
    public interface IDomainRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot, new()
    {
        TAggregateRoot GetById(Guid aggregateRootId);

        void Save(IAggregateRoot aggregateRoot);
    }
}