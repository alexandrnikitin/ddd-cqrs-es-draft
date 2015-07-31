using System;

namespace Octo.Core.Cqrs.Saga
{
    public interface ISaga
    {
        Guid Id { get; set; }
    }
}