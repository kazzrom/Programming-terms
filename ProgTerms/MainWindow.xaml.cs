using ProgTerms.Controllers;
using ProgTerms.Pages;
using ProgTerms.AppData;
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

namespace ProgTerms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConnectDB.Connect();
            MainObjects.FrameMain = FrameMain;
            MainObjects.FrameMain.Navigate(new PageMain());
            MainObjects.BtnBack = BtnBack;
            MainObjects.HeaderTextBlock = HeaderTextBlock;
            MainObjects.HeaderImage = HeaderImage;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MainObjects.IsMain)
            {
                MainObjects.FrameMain.Navigate(new PageMain());
                BtnBack.Visibility = Visibility.Collapsed;
                MainObjects.ChangeHeaderObjects("pack://application:,,,/Icons/Source Code.ico", "Главная");
            }
            else
            {
                MainObjects.FrameMain.Navigate(new PageAllTerms());
                MainObjects.IsMain = true;
                MainObjects.ChangeHeaderObjects("pack://application:,,,/Icons/Open Book.ico", "Все термины");
            }
        }
    }
}
