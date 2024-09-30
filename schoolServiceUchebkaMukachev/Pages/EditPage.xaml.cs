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
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        private Service ser;
        private string selectedImagePath;
        int charactersToRemove = 61; // Количество символов для удаления в начале строки
        public EditPage(Service service)
        {
            ser = service;
            InitializeComponent();
            var imagesBD = App.db.ServicePhoto.FirstOrDefault(x => x.ID == ser.ServicePhotoID).PhotoPath.ToString();
            string folderName = "schoolServiceUchebkaMukachev/Resource";
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string fullPath = System.IO.Path.Combine(projectDirectory, folderName, imagesBD);

            //Заменяем обратные слеши на прямые слеши
            ImageService.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
            TitleServiceTBox.Text = ser.Title.ToString();
            CostTBox.Text = ser.Cost.ToString();
            TimeTBox.Text = ser.DurationInMinutes.ToString();
            DiscountTBox.Text = ser.Discount.ToString();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.MainPage());
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            ser.Title = TitleServiceTBox.Text;
            ser.Cost = Convert.ToDecimal(CostTBox.Text);
            ser.DurationInMinutes = Convert.ToInt32(TimeTBox.Text);
            ser.Discount = Convert.ToInt32(DiscountTBox.Text);
            ser.ServicePhotoID = App.db.ServicePhoto.FirstOrDefault(x => x.PhotoPath == selectedImagePath).ID;
            App.db.SaveChanges();
            NavigationService.Navigate(new Pages.MainPage());
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
                if (!string.IsNullOrEmpty(selectedImagePath) && selectedImagePath.Length >= charactersToRemove)
                {
                    selectedImagePath = selectedImagePath.Substring(charactersToRemove);
                }
                ImageService.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
