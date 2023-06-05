using ProgTerms.Controllers;
using System;
using System.Collections.Generic;
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

namespace ProgTerms.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        public PageMain()
        {
            InitializeComponent();
        }

        private void BtnTerms_Click(object sender, RoutedEventArgs e)
        {
            MainObjects.FrameMain.Navigate(new PageAllTerms());
            MainObjects.IsMain = true;
            MainObjects.BtnBack.Visibility = Visibility.Visible;
            MainObjects.ChangeHeaderObjects("pack://application:,,,/Icons/Open Book.ico", "Все термины");
        }

        private void BtnFavoritesTerms_Click(object sender, RoutedEventArgs e)
        {
            MainObjects.FrameMain.Navigate(new PageFavoritesTerms());
            MainObjects.IsMain = true;
            MainObjects.BtnBack.Visibility = Visibility.Visible;
            MainObjects.ChangeHeaderObjects("pack://application:,,,/Icons/Bookmark.ico", "Избранное");
        }
    }
}
