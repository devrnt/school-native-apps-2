using System;
using System.Windows.Input;

namespace CityApp.Helpers
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private Action<object> execute;
        private Func<object, bool> canExecute = (_) => true;

        public RelayCommand(Action<object> action)
        {
            execute = action;
        }

        public RelayCommand(Action<object> action, Func<object, bool> test)
        {
            execute = action;
            canExecute = test;
        }


        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }


#pragma warning disable SA1402 // File may only contain a single class
    public class RelayCommand<T> : ICommand
#pragma warning restore SA1402 // File may only contain a single class
    {
        private readonly Action<T> _execute;

        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

}
