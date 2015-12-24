using System;

namespace BDM.App.UniversalApp.Mvvm.ViewModel
{
	public interface IPage<TViewModel> where TViewModel : BasePageViewModel
	{
		TViewModel ViewModel { get; set; }
	}
}
