using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tourista.View;

namespace Tourista
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Tour tour;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = tour;
            LoadTour();
            LoadCity_Regions();

        }

        public void LoadCity_Regions()
        {
            using (var db = new TouristaContext())
            {

                var cites = db.Cities.ToList();


                foreach (var item in cites)
                {
                    CityComboBox.Items.Add(item.CityName.ToString());
                    RegionComboBox.Items.Add(item.RegionName.ToString());
                }
            }
        }

        private void LoadTour()
        {
            using (var db = new TouristaContext())
            {
                //var tovars = db.Products.ToList();
                //TovarsDataGrid.ItemsSource = tovars;

                var tours = db.Tours
             .Include(t => t.City)          // Загружаем связанный город
             .Include(t => t.Bookings)     // Загружаем связанные бронирования (для подсчета участников)
             .Where(t => (bool)t.TourIsActive)    // Только активные туры
             .ToList();

                ProductItems.Items.Clear();


                foreach (var item in tours)
                {
                    ProductItems.Items.Add(new View.TourUserControl(item));
                }
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var searchText = SearchBox.Text.ToLower();
            //using (var db = new TouristaContext())
            //{
            //    var search = db.Tours
            //        .Include(t => t.IdVarietiesNavigation)          // Загружаем сорт
            //            .ThenInclude(v => v.IdViewNavigation)       // Загружаем вид для сорта
            //        .Include(t => t.IdSuppliersNavigation)          // Загружаем поставщика
            //        .Where(t =>
            //            t.IdVarietiesNavigation.VarietiesName.ToLower().Contains(searchText) ||
            //            t.IdVarietiesNavigation.IdViewNavigation.ViewName.ToLower().Contains(searchText))
            //        .ToList();

            //    ProductItems.Items.Clear();

            //    foreach (var item in search)
            //    {
            //        ProductItems.Items.Add(new Views.ProductUserControl(item));
            //    }
            //}

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Autorization autorization = new Autorization();
            autorization.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var searchText = SearchBox.Text.ToLower();
            using (var db = new TouristaContext())
            {
                var search = db.Tours
                    .Where(t => t.TourName.ToLower().Contains(searchText) || t.TourDescription.ToLower().Contains(searchText))
                    .ToList();
                ProductItems.Items.Clear(); foreach (var item in search)
                {
                    ProductItems.Items.Add(new View.TourUserControl(item));
                }
            }
        }

        private void Price_Increase_sort_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Price_Decrease_sort_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reset_filtr_Button_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
            LoadTour();
        }
    }
}