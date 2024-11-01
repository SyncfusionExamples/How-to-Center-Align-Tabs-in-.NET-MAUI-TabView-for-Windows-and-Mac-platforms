This article guides you on how to center align tabs in [.NET MAUI TabView](https://www.syncfusion.com/maui-controls/maui-tab-view) for Windows and Mac platforms.

To customize the placement of tabs to the center of the Windows and Mac screens, set [TabWidthMode](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.TabView.SfTabView.html#Syncfusion_Maui_TabView_SfTabView_TabWidthMode) to [SizeToContent](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.TabView.TabWidthMode.html#Syncfusion_Maui_TabView_TabWidthMode_SizeToContent) and adjust [TabHeaderPadding](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.TabView.SfTabView.html#Syncfusion_Maui_TabView_SfTabView_TabHeaderPadding) dynamically based on the screen's width. Below is a code snippet demonstrating this approach.

**XAML Code**

```xaml
<tabView:SfTabView TabWidthMode="SizeToContent">
…
<\tabView:SfTabView>
```

**C#**

```csharp
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
```

- The TabWidthMode is set to SizeToContent so that each tab's width is adjusted based on its content.
- The method `UpdateTabHeaderPadding` calculates the total width of all the tab items and dynamically adjusts the left padding of the tab headers to center them based on the screen width.

**Output**

![TabView_TabAlignment.gif](https://support.syncfusion.com/kb/agent/attachment/article/17398/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjI5MzYwIiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.99mAjs80Wwjabr15ABNoC5JX8l7RrCo36DsNBR5P4is)

**Requirements to run the demo**
 
To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements)
 
**Troubleshooting:**

**Path too long exception** 

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.