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
using schoolServiceUchebkaMukachev.Controls;
using schoolServiceUchebkaMukachev.DB;

namespace schoolServiceUchebkaMukachev.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public Action OnItemRemoved { get; set; }
        public MainPage()
        {
            InitializeComponent();
            DiscountRs.Minimum = Convert.ToDouble(App.db.Service.Min(i => i.Discount));
            DiscountRs.Maximum = Convert.ToDouble(App.db.Service.Max(i => i.Discount));
            DiscountRs.UpperValue = DiscountRs.Maximum;
            DiscountRs.LowerValue = DiscountRs.Minimum;
            //OnItemRemoved = () =>
            //{
            //    UpdatePage();
            //};
            //UpdatePage();
        }
        //public void UpdatePage()
        //{
          

        //    ServiceWpar.Children.Clear();
        //    foreach (var item in App.db.Service)
        //    {
        //        ServiceWpar.Children.Add(new ServiceUserControl(item, OnItemRemoved));
        //    }
        //}

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreatePage());
        }

        private void DiscountRs_RangeSelectionChanged(object sender, MahApps.Metro.Controls.RangeSelectionChangedEventArgs<double> e)
        {

        }

        

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null) return; // Защита от ошибок при неверном приведении типов

            var selectedSortOption = comboBox.SelectedItem?.ToString(); // Используйте оператор ?. для предотвращения ошибок при нулевом значении

            IEnumerable<Service> sortedServices;

            switch (selectedSortOption)
            {
                case "По цене (возрастание)":
                    sortedServices = App.db.Service.OrderBy(service => service.Cost);
                    break;
                case "По цене (убывание)":
                    sortedServices = App.db.Service.OrderByDescending(service => service.Cost);
                    break;
                
            }

            ServiceWpar.Children.Clear();
            foreach (var item in App.db.Service)
            {
                ServiceWpar.Children.Add(new ServiceUserControl(item, OnItemRemoved));
            }

        }
    }
}
