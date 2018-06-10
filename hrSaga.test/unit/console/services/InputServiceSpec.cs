using System;
using System.Collections.Generic;
using hrSaga.console.inputs;
using hrSaga.console.services;
using hrSaga.core.infra;
using Xunit;

namespace hrSaga.test.unit.console.services
{
    public class InputServiceSpec : IDisposable
    {
        InputService _inputService;

        class Test1Input : IInput
        {
            public string Value;

            public ICommand Command => null;

            public void Init(string args)
            {
                if (args != "wrong")
                {
                    Value = args;
                }
                else
                {
                    throw new ArgumentException("Wrong body");
                }
            }
        }

        class Test2Input : IInput
        {
            public ICommand Command => null;

            public void Init(string args)
            {
            }
        }

        public InputServiceSpec()
        {
            _inputService = new InputService(new List<Type> {
                typeof(Test1Input), typeof(Test2Input), typeof(Test2Input)
            });
        }

        [Fact]
        public void It_Should_Parse_An_Input()
        {
            var inputObj = _inputService.ParseInput("Test1 value1");

            Assert.NotNull(inputObj);
            Assert.IsType<Test1Input>(inputObj);
            Assert.Equal("value1", (inputObj as Test1Input).Value);
        }

        [Fact]
        public void It_Should_Return_Null_If_The_Arguments_Are_Wrong()
        {
            var inputObj = _inputService.ParseInput("Test1 wrong");

            Assert.Null(inputObj);
        }

        [Fact]
        public void It_Should_Return_Null_If_The_Input_Is_Not_Found()
        {
            var inputObj = _inputService.ParseInput("Test0");

            Assert.Null(inputObj);
        }

        [Fact]
        public void It_Should_Return_Null_If_The_Input_Is_Ambiguous()
        {
            var inputObj = _inputService.ParseInput("Test2");

            Assert.Null(inputObj);
        }

        public void Dispose()
        {
        }
    }
}
