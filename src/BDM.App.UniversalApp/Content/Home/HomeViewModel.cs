using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using BDM.App.UniversalApp.Utils.Navigation;
using BDM.Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BDM.App.UniversalApp.Content.Home
{
    public class HomeViewModel : BasePageViewModel
    {
        private BlaguesHelper _blaguesHelper;

        private Order _order = Order.Last;

        public ObservableCollection<Blague> Blagues { get; set; } = new ObservableCollection<Blague>();

        public HomeViewModel(BlaguesHelper blaguesHelper)
        {
            _blaguesHelper = blaguesHelper;
        }

        public bool ShowTopHeader
        {
            get
            {
                return new[] { Order.TopMonth, Order.TopWeek, Order.TopYear }.Contains(_order);
            }
        }

        public override async Task OnNavigatedTo(OwnNavigationEventArgs e)
        {
            if (!_blaguesHelper.HasBeenLoaded)
                await _blaguesHelper.LoadBlagues();

            Order order = Order.Last;
            if (e.Parameter is Order)
            {
                order = (Order)e.Parameter;
            }
            SetOrder(order);
        }

        public void SetOrder(Order order)
        {
            _order = order;
            
            Blagues.Clear();
            foreach (Blague blague in _blaguesHelper.GetBlagues(_order))
            {
                Blagues.Add(blague);
            }
            RaisePropertyChanged(() => ShowTopHeader);
            //RaisePropertyChanged(() => Blagues);
        }
    }
}
