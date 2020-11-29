using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyOwnLib.Common
{
    public class RelayCommand : ICommand
    {
        readonly Action<Object> _executeMethod;
        readonly Func<Object, Boolean> _canExecuteMethod;

        #region CTOR

        public RelayCommand(Action<Object> executeMethod)
        :this(executeMethod,null)
        {

        }

        public RelayCommand(Action<Object> executeMethod, Func<Object,Boolean> canExecuteMethod)
        {
            _executeMethod = executeMethod ?? throw new NullReferenceException("executeMethod is required");
            _canExecuteMethod = canExecuteMethod;
        }

        #endregion

        #region Methods

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter)
        {
            return _canExecuteMethod == null ? true : _canExecuteMethod(parameter);
        }

        public void Execute(object parameter)
        {
            _executeMethod(parameter);
        }

        #endregion


    }
}
