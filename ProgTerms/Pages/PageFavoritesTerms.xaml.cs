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

            GridFavoritesTerms.Visibility = Visibility.Hidden;
            TBNoneTerms.Visibility = Visibility.Visible;
            TBNoneTerms.Text = "Загрузка...";

            ListAllTerm.ItemsSource = ConnectDB.ProgTermsContext.Terms.Where(term => term.IsSave == 1).ToList();
            ListAllTerm.SelectedIndex = 0;
            SelectTerm();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += UpdateData;
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Start();
        }

        private void UpdateData(object? sender, EventArgs e)
        {
            TBNoneTerms.Text = "Загрузка...";
            var historyContext = ConnectDB.ProgTermsContext.Terms.Where(term => term.IsSave == 1).ToList();

            if (historyContext.Count == 0)
            {
                GridFavoritesTerms.Visibility = Visibility.Hidden;
                TBNoneTerms.Visibility = Visibility.Visible;
                TBNoneTerms.Text = "Список избранного пуст";
            }
            else
            {
                GridFavoritesTerms.Visibility = Visibility.Visible;
                TBNoneTerms.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrEmpty(WTBSearch.Text))
                ListAllTerm.ItemsSource = historyContext;
            else
                ListAllTerm.ItemsSource = historyContext.Where(term => term.Title.StartsWith(WTBSearch.Text)).ToList();
        }

        private void SelectTerm()
        {
            var selectTerm = ListAllTerm.SelectedItem as Term;
            if (selectTerm != null)
            {
                CurrentTerm.Term = selectTerm;
                TblTitle.Text = selectTerm.Title + " -";
                TblDefinion.Text = selectTerm.Definition;
                BtnBookmark.IsChecked = selectTerm.IsSave == 1;

                if (!string.IsNullOrEmpty(selectTerm.AddInformation))
                {
                    StkAddInfo.Visibility = Visibility.Visible;
                    TblAddInfo.Text = selectTerm.AddInformation;
                }
                else
                    StkAddInfo.Visibility = Visibility.Collapsed;
            }
        }

        private void ListAllTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListAllTerm.SelectedIndex != -1)
            {
                SelectTerm();
                ScrSelectedTerm.Visibility = Visibility.Visible;
                TblNoSelectTerm.Visibility = Visibility.Collapsed;
                MenuButtons.Visibility = Visibility.Visible;
            }
        }

        private void BtnEditTerm_Click(object sender, RoutedEventArgs e)
        {
            MainObjects.FrameMain.Navigate(new PageEditTerm(true));
            MainObjects.BtnBack.Visibility = Visibility.Collapsed;
        }

        private void BtnDeleteTerm_Click(object sender, RoutedEventArgs e)
        {
            var MsgBxIsDelete = MessageBox.Show("Вы действительно хотите удалить этот термин?", "Удаление термина",
                                                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (MsgBxIsDelete == MessageBoxResult.Yes)
            {
                ScrSelectedTerm.Visibility = Visibility.Collapsed;
                TblNoSelectTerm.Visibility = Visibility.Visible;
                MenuButtons.Visibility = Visibility.Hidden;

                ConnectDB.ProgTermsContext.Terms.Remove(CurrentTerm.Term);
                ConnectDB.ProgTermsContext.SaveChanges();
            }
        }

        private void BtnBookmark_Click(object sender, RoutedEventArgs e)
        {
            BtnBookmark.IsChecked = true;
            var MsgBxIsDelete = MessageBox.Show("Вы действительно хотите удалить этот термин из Избранного?", "Удаление термина из Избранного",
                                                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (MsgBxIsDelete == MessageBoxResult.Yes)
            {
                BtnBookmark.IsChecked = false;
                ScrSelectedTerm.Visibility = Visibility.Collapsed;
                TblNoSelectTerm.Visibility = Visibility.Visible;
                MenuButtons.Visibility = Visibility.Hidden;

                CurrentTerm.Term.IsSave = (bool)BtnBookmark.IsChecked! ? 1 : 0;
                ConnectDB.ProgTermsContext.SaveChanges();
            }
        }
    }
}
