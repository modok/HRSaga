using System;
using System.Linq;
using System.Reflection;
using hrSaga.console.inputs;
using hrSaga.console.services;
using hrSaga.core;

namespace hrSaga.console
{
    public class Application
    {
        readonly InputService _inputService;
        readonly HRSaga _hrSaga;

        public Application()
        {
            var inputType = typeof(IInput);
            var inputTypes = Assembly.GetExecutingAssembly().GetTypes()
                                     .Where(inputType.IsAssignableFrom);

            _inputService = new InputService(inputTypes);
            _hrSaga = new HRSaga();
        }

        public void Start()
        {
            Console.WriteLine("Welcome to HR Saga!");
            Console.WriteLine("Type 'quit' to terminate");
            Console.WriteLine("Type 'help' to show the help");
            Console.WriteLine();

            var quit = false;
            while (!quit)
            {
                Console.Write("command> ");
                switch (Console.ReadLine())
                {
                    case "quit": quit = true; break;
                    case "help": PrintHelp(); break;
                    case string input:
                        var inputObj = _inputService.ParseInput(input);
                        if (inputObj != null)
                        {
                            _hrSaga.PushCommand(inputObj.Command);
                        }
                        break;
                }
            }
        }

        void PrintHelp()
        {
            Console.WriteLine("HELP");
        }
    }
}
