using ProgTerms.Controllers;
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

namespace ProgTerms.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddTerm.xaml
    /// </summary>
    public partial class PageAddTerm : Page
    {
        public PageAddTerm()
        {
            InitializeComponent();
        }

        private void ClearForm()
        {
            WTBTitle.Clear();
            WTBDefinition.Clear();
            WTBAddInfo.Clear();
        }

        private void BtnAddTerm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(WTBTitle.Text) || string.IsNullOrEmpty(WTBDefinition.Text))
                {
                    WTBTitle.BorderBrush = string.IsNullOrEmpty(WTBTitle.Text) ? Brushes.Red : Brushes.Gray;
                    WTBDefinition.BorderBrush = string.IsNullOrEmpty(WTBDefinition.Text) ? Brushes.Red : Brushes.Gray;
                    throw new Exception("Введите обязательные поля!");
                }

                WTBTitle.BorderBrush = Brushes.Gray;
                WTBDefinition.BorderBrush = Brushes.Gray;

                int lastID = ConnectDB.ProgTermsContext.Terms.OrderBy(term => term.Id).Last().Id;

                ConnectDB.ProgTermsContext.Add(new Term()
                {
                    Id = lastID + 1,
                    Title = WTBTitle.Text,
                    Definition = WTBDefinition.Text,
                    AddInformation = WTBAddInfo.Text,
                });

                ConnectDB.ProgTermsContext.SaveChanges();
                ClearForm();
                MessageBox.Show("Термин успешно добавлен!", "Добавление термина", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
