using System;
using System.Diagnostics;

namespace HSEBankFinances.Commands
{
    public class TimingCommandDecorator : ICommand
    {
        private readonly ICommand _innerCommand;

        public TimingCommandDecorator(ICommand innerCommand)
        {
            _innerCommand = innerCommand;
        }

        public void Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            _innerCommand.Execute();
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения команды {_innerCommand.GetType().Name}: {stopwatch.ElapsedMilliseconds} мс");
        }
    }
}