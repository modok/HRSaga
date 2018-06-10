using System;
using hrSaga.core.infra;
using Xunit;

namespace hrSaga.test.unit.core.infra
{
    public class CommandBusSpec : IDisposable
    {
        CommandBus _commandBus;

        class TestCommand : ICommand
        {
            public string Value;
        }

        public CommandBusSpec()
        {
            _commandBus = new CommandBus();
        }

        [Fact]
        public void It_Should_Be_Possible_To_Register_To_A_Command()
        {
            var commandValue = "";

            _commandBus.RegisterToCommand((TestCommand e) => { commandValue = e.Value; });
            _commandBus.PushCommand(new TestCommand { Value = "test1" });

            Assert.Equal("test1", commandValue);
        }

        [Fact]
        public void It_Should_Not_Allow_Multiple_Registrations()
        {
            var commandValue = "";

            Assert.Throws<ArgumentException>(() =>
            {
                _commandBus.RegisterToCommand((TestCommand e) => { commandValue = e.Value; });
                _commandBus.RegisterToCommand((TestCommand e) => { commandValue = e.Value; });
            });
        }

        public void Dispose()
        {
        }
    }
}
