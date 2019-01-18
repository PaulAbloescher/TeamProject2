using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace YALS_WaspEdition.Commands
{
    public class Command : ICommand
    {
        private readonly Action<object> execute;

        public Command(Action<object> execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
