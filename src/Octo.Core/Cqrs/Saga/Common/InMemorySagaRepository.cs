using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Octo.Core.Cqrs.Saga.Common
{
    internal class InMemorySagaRepository : ISagaRepository
    {
        private readonly IDictionary<Guid, ISaga> _sagas = new ConcurrentDictionary<Guid, ISaga>();

        public ISaga GetBy(Guid sagaId)
        {
            ISaga saga;
            return _sagas.TryGetValue(sagaId, out saga) ? saga : null;
        }
    }
}