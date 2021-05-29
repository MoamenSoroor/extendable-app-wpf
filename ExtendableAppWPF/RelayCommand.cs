using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExtendableAppWPF
{
    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event Action<T> ExecuteHandler;
        public event Func<T,bool> CanExecuteHandler;

        public bool CanExecute(object parameter)
        {
            return CanExecuteHandler?.Invoke((T)parameter)??true;
        }

        public void Execute(object parameter)
        {
            ExecuteHandler.Invoke((T)parameter);
        }
    }
}
