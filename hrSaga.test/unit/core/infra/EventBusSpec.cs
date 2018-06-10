using System;
using System.Collections.Generic;
using hrSaga.core.infra;
using Xunit;

namespace hrSaga.test.unit.core.infra
{
    public class EventBusSpec : IDisposable
    {
        EventBus _eventBus;

        class TestEvent : IEvent
        {
            public string Value;
        }

        public EventBusSpec()
        {
            _eventBus = new EventBus();
        }

        [Fact]
        public void It_Should_Be_Possible_To_Register_To_An_Event()
        {
            var eventValues = new List<String>();

            _eventBus.RegisterToEvent((TestEvent e) => { eventValues.Add(e.Value); });
            _eventBus.RegisterToEvent((TestEvent e) => { eventValues.Add(e.Value.ToUpper()); });
            _eventBus.PushEvent(new TestEvent { Value = "test1" });
            _eventBus.PushEvent(new TestEvent { Value = "test2" });

            Assert.Equal(new List<String> { "test1", "TEST1", "test2", "TEST2" }, eventValues);
        }

        public void Dispose()
        {
        }
    }
}
