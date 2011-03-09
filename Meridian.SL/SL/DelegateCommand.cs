using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Meridian.SL
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> executeMethod = null;
        private readonly Func<T, Boolean> canExecuteMethod = null;
        private readonly Dispatcher dispatcher;

        public DelegateCommand(Action<T> executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, Boolean> canExecuteMethod)
        {
            Requires.NotNull(executeMethod, "executeMethod");
            Requires.NotNull(canExecuteMethod, "canExecuteMethod");

            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
            if (Application.Current != null)
            {
                dispatcher = Application.Current.RootVisual.Dispatcher;
            }
        }

        public Boolean CanExecute(T parameter)
        {
            if (canExecuteMethod == null)
            {
                return true;
            }
            return canExecuteMethod(parameter);
        }

        public void Execute(T parameter)
        {
            if (executeMethod != null)
            {
                executeMethod(parameter);
            }
        }

        Boolean ICommand.CanExecute(Object parameter)
        {
            return CanExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged;

        void ICommand.Execute(Object parameter)
        {
            Execute((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler canExecuteChangedHandler = CanExecuteChanged;
            if (canExecuteChangedHandler != null)
            {
                if (dispatcher != null && !dispatcher.CheckAccess())
                {
                    dispatcher.BeginInvoke((Action)OnCanExecuteChanged);
                }
                else
                {
                    canExecuteChangedHandler(this, EventArgs.Empty);
                }
            }
        }
    }
}
