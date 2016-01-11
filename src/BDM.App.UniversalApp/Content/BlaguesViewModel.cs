using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.App.UniversalApp.Utils;
using BDM.App.UniversalApp.Utils.Navigation;
using BDM.App.UniversalApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;

namespace BDM.App.UniversalApp.Content
{
    public abstract class BlaguesViewModel : BasePageViewModel
    {
        protected BlaguesHelper _blaguesHelper;

        private SharingObject _sharingObject;

        protected override bool _hasSharing => true;

        public ObservableCollection<BlagueVM> Blagues { get; set; } = new ObservableCollection<BlagueVM>();

        public BlaguesViewModel(BlaguesHelper blaguesHelper)
        {
            _blaguesHelper = blaguesHelper;
        }

        protected async Task CheckBlaguesHelper()
        {
            if (!_blaguesHelper.HasBeenLoaded)
                await _blaguesHelper.LoadBlagues();
        }

        public abstract Task ReloadBlagues();

        public async void Vote(BlagueVM blague, bool like)
        {
            bool vote = await _blaguesHelper.Vote(blague.Id, like);
            string message = vote ? string.Format("Tu {0} aimé cette blague", like ? "as" : "n'as pas") : "Tu as déjà voté pour cette blague.";

            if (vote)
            {
                if (like) { blague.NbGoods++; }
                else { blague.NbBads++; }
            }
            await App.Current.GetShell().ShowNotificationAsync(message);
        }

        public void SetSharingObject(BlagueVM blague)
        {
            if (blague != null)
            {
                _sharingObject = new SharingObject()
                {
                    Title = "Une blague de merde",
                    Text = blague.Text,
                    SharingType = SharingType.Text
                };
            }
            else
            {
                _sharingObject = null;
            }
        }

        public override SharingObject OnSharing()
        {
            return _sharingObject;
        }
    }
}
