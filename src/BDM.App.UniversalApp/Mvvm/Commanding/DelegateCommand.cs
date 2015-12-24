using System;

namespace BDM.App.UniversalApp.Mvvm.Commanding
{
	public class DelegateCommand : BaseDelegateCommand<object>
	{
		public DelegateCommand(Action execute, Func<bool> canExecute = null)
			: base(execute != default(Action) ? _ => execute() : default(Action<object>),
				_ => (canExecute ?? (() => true))())
		{
		}
	}

	public class DelegateCommand<T> : BaseDelegateCommand<T>
	{
		public DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null)
			: base(execute, canExecute)
		{
		}
	}
}
