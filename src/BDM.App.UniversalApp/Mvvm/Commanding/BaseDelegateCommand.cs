using System;
using System.Windows.Input;

namespace BDM.App.UniversalApp.Mvvm.Commanding
{
	public abstract class BaseDelegateCommand<T> : ICommand
	{
		private readonly Action<T> _execute;
		private readonly Func<T, bool> _canExecute;

		protected BaseDelegateCommand(Action<T> execute, Func<T, bool> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");

			_execute = execute;
			_canExecute = canExecute ?? (_ => true);
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute((T)parameter);
		}

		public void Execute(object parameter)
		{
			_execute((T)parameter);
		}

		public event EventHandler CanExecuteChanged;

		public void RaiseCanExecuteChanged()
		{
			var handler = CanExecuteChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}
	}

	
}
