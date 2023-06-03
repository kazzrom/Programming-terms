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
                    throw new Exception("Введите обязательные поля!");

                ConnectDB.ProgTermsContext.Add(new Term()
                {
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
