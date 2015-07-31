using System;

namespace Octo.Core.Cqrs.Saga
{
    public interface ISagaRepository
    {
        ISaga GetBy(Guid sagaId);
    }
}