using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using BDM.App.UniversalApp.Utils.Navigation;
using BDM.Common.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BDM.App.UniversalApp.Content.Categories
{
    public class CategoriesViewModel : BasePageViewModel
    {
        private BlaguesHelper _blaguesHelper;

        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        public CategoriesViewModel(BlaguesHelper blaguesHelper)
        {
            _blaguesHelper = blaguesHelper;
        }

        public override async Task OnNavigatedTo(OwnNavigationEventArgs e)
        {
            var categories = await _blaguesHelper.GetCategories();
            Categories.Clear();

            foreach (var cat in categories)
                Categories.Add(cat);
        }
    }
}
