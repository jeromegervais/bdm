using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using BDM.App.UniversalApp.ViewModels;
using BDM.Common.Model;
using BDM.Data.Client.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace BDM.App.UniversalApp.Content.Search
{
    public class SearchViewModel : BlaguesViewModel
    {
        public SearchViewModel(BlaguesHelper blaguesHelper)
            : base(blaguesHelper)
        {

        }

        public override async Task OnNavigatedFrom(NavigationEventArgs e)
        {
            SearchWord = string.Empty;
            Blagues.Clear();
            await base.OnNavigatedFrom(e);
        }

        public string SearchWord { get; set; }

        public async Task Search()
        {
            Blagues.Clear();

            List<BlagueVM> blagues = await _blaguesHelper.Search(SearchWord);
            
            foreach (BlagueVM blague in blagues)
            {
                Blagues.Add(blague);
            }
        }
    }
}
