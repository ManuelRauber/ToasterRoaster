using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.Foundation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Size = Windows.Foundation.Size;

namespace ToasterRoaster.WindowsPhone
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();

      var media = new MediaElement();
      DrawingSurface.Children.Add(media);

      var application = new GameRenderer(new Size(App.Current.Host.Content.ActualWidth,
        App.Current.Host.Content.ActualHeight), 100, media);

      DrawingSurface.SetBackgroundContentProvider(application.ContentProvider);
      DrawingSurface.SetBackgroundManipulationHandler(application.ManipulationHandler);
    }

    // Sample code for building a localized ApplicationBar
    //private void BuildLocalizedApplicationBar()
    //{
    //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
    //    ApplicationBar = new ApplicationBar();

    //    // Create a new button and set the text value to the localized string from AppResources.
    //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
    //    appBarButton.Text = AppResources.AppBarButtonText;
    //    ApplicationBar.Buttons.Add(appBarButton);

    //    // Create a new menu item with the localized string from AppResources.
    //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
    //    ApplicationBar.MenuItems.Add(appBarMenuItem);
    //}
  }
}