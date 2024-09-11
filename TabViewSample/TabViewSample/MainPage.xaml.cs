#if WINDOWS
using Microsoft.UI.Windowing;
#elif MACCATALYST
using UIKit;
#endif

namespace TabViewSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

#if WINDOWS
            var window = GetParentWindow().Handler.PlatformView as MauiWinUIWindow;
            if (window != null)
            {
                UpdateTabHeaderPadding(window.Bounds.Width);
            }
#elif MACCATALYST
            var macWindow = GetParentWindow().Handler.PlatformView as UIWindow;
            if (macWindow != null)
            {
                UpdateTabHeaderPadding(macWindow.Bounds.Width);
            }
#endif
        }

#if WINDOWS || MACCATALYST
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            UpdateTabHeaderPadding(width);
        }
        private void UpdateTabHeaderPadding(double width)
        {
            double totalTabWidth = 0;
            foreach (var tabItem in tabView.Items)
            {
                if (tabItem is Syncfusion.Maui.TabView.SfTabItem tabItemView)
                {
                    totalTabWidth += tabItemView.Measure(double.PositiveInfinity, double.PositiveInfinity).Request.Width;
                }
            }
            double remainingSpace = ((width - totalTabWidth));

            double padding = (remainingSpace / 2) ;

            tabView.TabHeaderPadding = new Thickness(padding, 0, 0, 0);
        }
#endif
    }

}

