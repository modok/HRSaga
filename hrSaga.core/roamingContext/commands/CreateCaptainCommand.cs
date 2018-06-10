using System;
using hrSaga.core.infra;

namespace hrSaga.core.roamingContext.commands
{
    public class CreateCaptainCommand : ICommand
    {
        public Guid Id { get; }

        public CreateCaptainCommand(Guid id)
        {
            Id = id;
        }
    }
}
