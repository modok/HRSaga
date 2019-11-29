namespace HRSaga.Context.Common.Domain.Model
{
    using System;

    public interface IDomainEvent
    {
        int EventVersion { get; set;  }
        DateTime OccurredOn { get; set; }
    }
}
