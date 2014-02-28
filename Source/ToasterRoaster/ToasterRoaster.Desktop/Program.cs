using System;

namespace ToasterRoaster.Desktop
{
  public static class Program
  {
    [STAThread]
    public static void Main()
    {
      using (var app = new Application())
      {
        app.Run();
      }
    }
  }
}

