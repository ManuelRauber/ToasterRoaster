using WaveEngine.Components.UI;

namespace ToasterRoaster.Game.Common
{
  public static class UIExtensions
  {
    public static void SetGridProperties(this UIBase control, int row, int column)
    {
      control.SetValue(GridControl.ColumnProperty, column);
      control.SetValue(GridControl.RowProperty, row);
    }
  }
}