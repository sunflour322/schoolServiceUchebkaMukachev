using Microsoft.Win32;
using schoolServiceUchebkaMukachev.DB;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace schoolServiceUchebkaMukachev.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreatePage.xaml
    /// </summary>
    public partial class CreatePage : Page
    {
        private string selectedImagePath;
        public string folderName = "Услуги школы";
        public CreatePage()
        {
            InitializeComponent();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            Service ser = new Service();
            if (TitleServiceTBox.Text != "" && CostTBox.Text != "" && TimeTBox.Text != "" && DiscountTB.Text != "" && selectedImagePath != null)
            {

                ser.Title = TitleServiceTBox.Text;
                ser.Cost = Convert.ToDecimal(CostTBox.Text);
                ser.DurationInMinutes = Convert.ToInt32(TimeTBox.Text);
                ser.Discount = Convert.ToInt32(DiscountTBox.Text);
                ser.ServicePhotoID = App.db.ServicePhoto.FirstOrDefault(x => x.PhotoPath == selectedImagePath).ID;
                App.db.Service.Add(ser);
                App.db.SaveChanges();
                NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Заполните данные");
            }
        }
        private void Button_Click_Image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                selectedImagePath = openFileDialog.FileName;
                // Ищем индекс папки "Услуги школы" и обрезаем строку
                int index = selectedImagePath.IndexOf(folderName);
                if (index != -1)
                {
                    string result = selectedImagePath.Substring(index); selectedImagePath = result;
                }
                ImageService.Source = new BitmapImage(new Uri(openFileDialog.FileName));

            }
        }
    }
}
