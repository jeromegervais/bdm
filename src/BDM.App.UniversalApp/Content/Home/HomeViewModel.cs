using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using BDM.App.UniversalApp.Utils.Navigation;
using BDM.Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI.Text;

namespace BDM.App.UniversalApp.Content.Home
{
    public class HomeViewModel : BlaguesViewModel
    {
        
        private Order _order = Order.Last;
        
        public HomeViewModel(BlaguesHelper blaguesHelper)
            :base(blaguesHelper)
        {
            
        }

        public bool ShowTopHeader => new[] { Order.TopMonth, Order.TopWeek, Order.TopYear }.Contains(_order);

        public FontWeight WeekFontWeight => _order == Order.TopWeek ? FontWeights.Bold : FontWeights.Normal;
        public FontWeight MonthFontWeight => _order == Order.TopMonth ? FontWeights.Bold : FontWeights.Normal;
        public FontWeight YearFontWeight => _order == Order.TopYear ? FontWeights.Bold : FontWeights.Normal;

        public string OrderName => (typeof(Order)).GetTypeInfo().GetDeclaredField(_order.ToString()).GetCustomAttribute<DisplayAttribute>().Name;

        public override async Task OnNavigatedTo(OwnNavigationEventArgs e)
        {
            await CheckBlaguesHelper();
            SetFromOrder((e.Parameter as Order?) ?? Order.Last);
        }

        public void SetFromOrder(Order? order = null)
        {
            if (order.HasValue)
            {
                _order = order.Value;
            }
            Blagues.Clear();
            foreach (Blague blague in _blaguesHelper.GetBlagues(_order))
            {
                Blagues.Add(blague);
            }
            RaisePropertyChanged(() => ShowTopHeader);
            RaisePropertyChanged(() => WeekFontWeight);
            RaisePropertyChanged(() => MonthFontWeight);
            RaisePropertyChanged(() => YearFontWeight);
            RaisePropertyChanged(() => OrderName);
        }

        public override async Task ReloadBlagues()
        {
            await _blaguesHelper.LoadBlagues();
            SetFromOrder();
        }
    }
}
