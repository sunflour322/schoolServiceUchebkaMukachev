using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public Client client;
        public DbSet<Client> currentUser;
        public MainPage(Client currentUser)
        {
            InitializeComponent();
            client = currentUser;
            var minDiscount = App.db.Service.Where(i => i.Discount > 0).Min(i => (int?)i.Discount) ?? 0;

            // Устанавливаем минимальное значение для DiscountRs, если найденное минимальное больше нуля
            DiscountRs.Minimum = Convert.ToDouble(minDiscount);
            DiscountRs.Maximum = Convert.ToDouble(App.db.Service.Max(i => i.Discount));
            DiscountRs.UpperValue = DiscountRs.Maximum;
            DiscountRs.LowerValue = DiscountRs.Minimum;
            var  services = App.db.Service.ToList();
            LoadServices();
        }
        private void LoadServices()
        {
            var services = App.db.Service.ToList(); // Получаем список услуг
            UpdatePage(services); // Загружаем в интерфейс
        }
        public void UpdatePage(List<Service> services)
        {
            ServiceWpar.Children.Clear();
            foreach (var item in services)
            {
                ServiceWpar.Children.Add(new ServiceUserControl(item, () => UpdatePage(App.db.Service.ToList()),currentUser));
            }
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreatePage());
        }

        private void DiscountRs_RangeSelectionChanged(object sender, MahApps.Metro.Controls.RangeSelectionChangedEventArgs<double> e)
        {
            ServiceWpar.Children.Clear();
            Sort();
        }

        public void Sort()
        {
            
                // Получаем текст для поиска по названию из TextBox (предполагается, что у вас есть TextBox для поиска)
                var searchText = SearchTb.Text.ToLower(); // Приводим к нижнему регистру для корректного сравнения

                // Получаем данные, фильтруя по диапазону скидок и по названию
                var filteredServices = App.db.Service.Where(i =>
                    i.Discount >= DiscountRs.LowerValue &&        // Фильтрация по скидкам
                    i.Discount <= DiscountRs.UpperValue &&        // Фильтрация по скидкам
                    i.Title.ToLower().Contains(searchText))       // Фильтрация по названию (поиск)
                    .ToList();

                // Применяем сортировку в зависимости от выбранного элемента ComboBox
                var selectedSortOption = PriceCb.SelectedIndex; // Предполагается, что есть ComboBox для сортировки

                switch (selectedSortOption)
                {
                    case 0: // Сортировка по цене (возрастание)
                        filteredServices = filteredServices.OrderBy(service => service.Cost).ToList();
                        break;
                    case 1: // Сортировка по цене (убывание)
                        filteredServices = filteredServices.OrderByDescending(service => service.Cost).ToList();
                        break;
                        // Можете добавить другие варианты сортировки, если нужно
                }

                // Обновляем отображение с отфильтрованными и отсортированными данными
                UpdatePage(filteredServices);
            
        }
        

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceWpar.Children.Clear();
            Sort();

        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServiceWpar.Children.Clear();
            Sort();
        }
    }
}
