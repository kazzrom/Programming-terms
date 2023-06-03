using ProgTerms.AppData;
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
    /// Логика взаимодействия для PageEditTerm.xaml
    /// </summary>
    public partial class PageEditTerm : Page
    {
        private bool IsFavorite { get; set; }
        public PageEditTerm(bool isFavorite)
        {
            InitializeComponent();
            IsFavorite = isFavorite;
            WTBTitle.Text = CurrentTerm.Term.Title;
            WTBDefinition.Text = CurrentTerm.Term.Definition; 
            WTBAddInfo.Text = CurrentTerm.Term.AddInformation;
        }

        private void BtnSaveTerm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(WTBTitle.Text) || string.IsNullOrEmpty(WTBDefinition.Text))
                    throw new Exception("Введите обязательные поля!");

                var MBisSave = MessageBox.Show("Сохранить изменения?", "Изменение термина", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if(MBisSave == MessageBoxResult.Yes)
                {
                    CurrentTerm.Term.Title = WTBTitle.Text;
                    CurrentTerm.Term.Definition = WTBDefinition.Text;
                    CurrentTerm.Term.AddInformation = WTBAddInfo.Text;

                    ConnectDB.ProgTermsContext.SaveChanges();
                    Properties.Frame.Navigate(IsFavorite ? new PageFavoritesTerms() : new PageTerm());
                }
                else if (MBisSave == MessageBoxResult.No)
                    Properties.Frame.Navigate(IsFavorite ? new PageFavoritesTerms() : new PageTerm());
                Properties.BtnBack.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
