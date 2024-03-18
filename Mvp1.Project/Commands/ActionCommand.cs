using System;
using System.Windows.Input;

namespace Mvp1.Project.Commands
{
    public class ActionCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public ActionCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => canExecute(parameter);

        public void Execute(object parameter) => execute(parameter);

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}