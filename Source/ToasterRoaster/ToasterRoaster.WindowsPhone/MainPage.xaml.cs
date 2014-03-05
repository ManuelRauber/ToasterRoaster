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
  }
}