using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BDM.App.UniversalApp.Controls
{
    public sealed partial class ListBlaguesUserControl : UserControl
    {
        public event RoutedEventHandler VoteClick;
        public event RoutedEventHandler ShareClick;

        public ListBlaguesUserControl()
        {
            this.InitializeComponent();
        }

        private void Vote_Click(object sender, RoutedEventArgs e)
        {
            VoteClick?.Invoke(sender, e);
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            ShareClick?.Invoke(sender, e);
        }
    }
}
