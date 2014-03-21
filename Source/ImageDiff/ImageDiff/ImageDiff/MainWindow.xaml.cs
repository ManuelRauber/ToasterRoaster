using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageDiff
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Console.WriteLine(getComparisonRate(new Bitmap("bild1.bmp"), new Bitmap("bild2.bmp")));
            Console.WriteLine(getComparisonRate(new Bitmap("bild1.bmp"), new Bitmap("bild3.bmp")));
            Console.WriteLine(getComparisonRate(new Bitmap("bild1.bmp"), new Bitmap("bild4.bmp")));
            Console.WriteLine(getComparisonRate(new Bitmap("bild1.bmp"), new Bitmap("bild5.bmp")));
        }

        private double getComparisonRate(Bitmap bmp1, Bitmap bmp2)
        {
            double equalCount = 0;
            if (bmp1.Size == bmp2.Size)
            {
                for (int x = 0; x < bmp1.Width; ++x)
                {
                    for (int y = 0; y < bmp1.Height; ++y)
                    {
                        if (bmp1.GetPixel(x, y) == bmp2.GetPixel(x, y))
                        {
                            equalCount++;
                        }
                    }
                }
            }
            else
            {
                //fatal error exception, pictures have no equal sizes (different width or height in pixel)
            }
            return (equalCount /( bmp1.Size.Height*bmp1.Size.Width));
        }

    }
}
