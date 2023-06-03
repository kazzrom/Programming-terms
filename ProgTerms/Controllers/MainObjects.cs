using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProgTerms.Controllers
{
    public static class MainObjects
    {
        public static Frame Frame { get; set; }
        public static bool IsMain { get; set; }
        public static Button BtnBack { get; set; }
        public static Image HeaderImage { get; set; }
        public static TextBlock HeaderTextBlock{ get; set; }
        public static void ChangeHeaderObjects(string pathImage, string text)
        {
            HeaderImage.Source = new BitmapImage(new Uri(pathImage));
            HeaderTextBlock.Text = text;
        }
    }
}
