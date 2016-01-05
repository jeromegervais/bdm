using BDM.App.UniversalApp.Utils;
using BDM.App.UniversalApp.Utils.Navigation;
using BDM.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDM.App.UniversalApp.Content.Categories
{
    public class CategoryPageViewModel : BlaguesViewModel
    {
        private Category _category = null;

        public string CategoryName => _category?.Name ?? string.Empty;

        public CategoryPageViewModel(BlaguesHelper blaguesHelper)
            :base(blaguesHelper)
        {

        }
        
        public override async Task OnNavigatedTo(OwnNavigationEventArgs e)
        {
            await CheckBlaguesHelper();

            if (e.Parameter is Category)
            {
                _category = (Category)e.Parameter;
                SetFromCategory();
            }
        }
        
        public async void SetFromCategory()
        {
            Blagues.Clear();

            var blagues = await _blaguesHelper.GetBlaguesForCategory(_category.Id);

            foreach (Order order in new[] { Order.Random, Order.TopMonth, Order.TopYear })
            {
                List<Blague> list;
                if (blagues.TryGetValue(order, out list) && list.Any())
                {
                    foreach (Blague blague in list)
                    {
                        Blagues.Add(blague);
                    }
                    break;
                }
            }
            RaisePropertyChanged(() => CategoryName);
        }

        public override async Task ReloadBlagues()
        {
            await _blaguesHelper.LoadBlagues();
            SetFromCategory();
        }
    }
}
