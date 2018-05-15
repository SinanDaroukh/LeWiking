using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wiking.Presentation.Commands
{
    public class SimpleCommand : ICommand
    {
        private readonly Action<Object> _executeInternal;
        private readonly Predicate<object> _canExecute;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _executeInternal(parameter);
        }

        public SimpleCommand(Action<object> execute)
        {
            _executeInternal = execute;
            _canExecute = (h) => { return true; };
        }

        public SimpleCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _executeInternal = execute;
            _canExecute = CanExecute;
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
