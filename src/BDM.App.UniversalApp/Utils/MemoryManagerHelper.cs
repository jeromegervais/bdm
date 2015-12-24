using System;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace BDM.App.UniversalApp.Utils
{
    public static class MemoryManagerHelper
    {
        private static Popup _popup;
        private static StackPanel _stackPanel;
        private static TextBlock _memoryConsumption;
        private static TextBlock _memoryMax;

        private static DispatcherTimer _timer;

        private static bool _isInit = false;

        static MemoryManagerHelper()
        {
            _popup = new Popup();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += (sender, e) =>
            {
                Update();
            };
            _timer.Start();
        }

        public static void Init()
        {
            _popup.IsHitTestVisible = false;

            _stackPanel = new StackPanel();
            _stackPanel.Orientation = Orientation.Horizontal;
            _stackPanel.Background = new SolidColorBrush(Colors.Black);
            _stackPanel.Opacity = 0.8;

            _memoryConsumption = new TextBlock();
            _memoryConsumption.Foreground = new SolidColorBrush(Colors.White);


            _memoryMax = new TextBlock();
            _memoryMax.Foreground = new SolidColorBrush(Colors.Red);
            _memoryMax.Text = " / " + (MemoryManager.AppMemoryUsageLimit / 1024.0 / 1024.0).ToString();

            _stackPanel.Children.Add(_memoryConsumption);
            _stackPanel.Children.Add(_memoryMax);

            _popup.Child = _stackPanel;
            _popup.IsOpen = true;

            _isInit = true;
        }

        public static void Update()
        {
            if (_isInit)
            {
                _memoryConsumption.Text = (MemoryManager.AppMemoryUsage / 1024.0 / 1024.0).ToString("0.0");
            }
            else
            {
                _timer.Stop();
            }
        }
    }
}
