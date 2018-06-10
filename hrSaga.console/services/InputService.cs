using System;
using System.Collections.Generic;
using System.Linq;
using hrSaga.console.inputs;

namespace hrSaga.console.services
{
    public class InputService
    {
        readonly IEnumerable<Type> _inputTypes;

        public InputService(IEnumerable<Type> inputTypes)
        {
            _inputTypes = inputTypes;
        }

        public IInput ParseInput(string input)
        {
            IInput result = null;

            var inputName = input.Split(" ").First();
            var matchingInputTypes = _inputTypes
                .Where(inputType => IsMatching(inputType, inputName));

            if (!matchingInputTypes.Any())
            {
                Console.WriteLine($"Command {inputName} is not defined");
            }
            else if (matchingInputTypes.Count() > 1)
            {
                Console.WriteLine($"Command {inputName} is ambiguous");
            }
            else
            {
                var inputType = matchingInputTypes.First();
                var inputArgs = input.Substring(inputName.Length).Trim();
                var inputObj = Activator.CreateInstance(inputType) as IInput;
                try
                {
                    inputObj.Init(inputArgs);
                    result = inputObj;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Command {inputName}: {e.Message}");
                }
            }

            return result;
        }

        bool IsMatching(Type inputType, string inputName)
        {
            return inputType.Name == $"{inputName}Input";
        }
    }
}
