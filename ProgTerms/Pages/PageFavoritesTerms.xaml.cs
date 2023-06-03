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
using System.Windows.Threading;

namespace ProgTerms.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageFavoritesTerms.xaml
    /// </summary>
    public partial class PageFavoritesTerms : Page
    {
        public PageFavoritesTerms()
        {
            InitializeComponent();

            NoneTerm();
            ListAllTerm.ItemsSource = ConnectDB.ProgTermsContext.Terms.Where(term => term.IsSave).ToList();
            ListAllTerm.SelectedIndex = 0;
            SelectTerm();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += UpdateData;
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Start();
        }

        private void UpdateData(object? sender, EventArgs e)
        {
            var historyContext = ConnectDB.ProgTermsContext.Terms.Where(term => term.IsSave).ToList();
            if (string.IsNullOrEmpty(WTBSearch.Text))
                ListAllTerm.ItemsSource = historyContext;
            else
                ListAllTerm.ItemsSource = historyContext.Where(term => term.Title.StartsWith(WTBSearch.Text)).ToList();

            if (historyContext.Count == 0)
                NoneTerm();
        }

        private void SelectTerm()
        {
            var selectTerm = ListAllTerm.SelectedItem as Term;
            if (selectTerm != null)
            {
                CurrentTerm.Term = selectTerm;
                TblTitle.Text = selectTerm.Title + " -";
                TblDefinion.Text = selectTerm.Definition;
                BtnBookmark.IsChecked = selectTerm.IsSave;

                if (!string.IsNullOrEmpty(selectTerm.AddInformation))
                {
                    StkAddInfo.Visibility = Visibility.Visible;
                    TblAddInfo.Text = selectTerm.AddInformation;
                }
                else
                    StkAddInfo.Visibility = Visibility.Collapsed;
            }
        }

        private void NoneTerm()
        {
            TblTitle.Text = "Нет терминов";
            TblDefinion.Text = "Отсутствует";
            StkAddInfo.Visibility = Visibility.Collapsed;
            BtnBookmark.IsChecked = false;
        }

        private void ListAllTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectTerm();
        }

        private void BtnEditTerm_Click(object sender, RoutedEventArgs e)
        {
            MainObjects.Frame.Navigate(new PageEditTerm(true));
            MainObjects.BtnBack.Visibility = Visibility.Collapsed;
        }

        private void BtnDeleteTerm_Click(object sender, RoutedEventArgs e)
        {
            var MBisDelete = MessageBox.Show("Вы действительно хотите удалить этот термин?", "Удаление термина",
                                                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (MBisDelete == MessageBoxResult.Yes)
            {
                ConnectDB.ProgTermsContext.Terms.Remove(CurrentTerm.Term);
                ConnectDB.ProgTermsContext.SaveChanges();
                ListAllTerm.SelectedIndex = 1;
                SelectTerm();
            }
        }

        private void BtnBookmark_Click(object sender, RoutedEventArgs e)
        {
            if (TblTitle.Text != "Нет терминов")
            {
                CurrentTerm.Term.IsSave = (bool)BtnBookmark.IsChecked!;
                ConnectDB.ProgTermsContext.SaveChanges();
            }
        }
    }
}
