using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneNote.Application.ViewModel
{
    public class ClickCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private bool _canExecute = false;

        public delegate void ExecuteDelegate(object param);
        public event ExecuteDelegate OnExecute;

        public ClickCommand() { }
        public ClickCommand(ExecuteDelegate d) { OnExecute += d; }

        public void SetCanExecuted(bool canExecute)
        {
            _canExecute = canExecute;
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            OnExecute?.Invoke(parameter);
        }
    }
}
