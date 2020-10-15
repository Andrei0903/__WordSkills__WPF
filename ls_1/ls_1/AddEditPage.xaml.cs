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

namespace ls_1
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {

        private Hotel _correntHotel = new Hotel();

        public AddEditPage()
        {
            InitializeComponent();
            DataContext = _correntHotel;
             
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            ComboCountry.ItemsSource = TourBaseEntities.GetContext().Country.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_correntHotel.Name))
                errors.AppendLine("Укажите название отеля");
            if (_correntHotel.CountOfStars < 1 || _correntHotel.CountOfStars > 5)
                errors.AppendLine("Число звезд от 1 до 5");
            if (_correntHotel.Country == null)
                errors.AppendLine("Выберите страну");
           
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_correntHotel.id == 0)
                TourBaseEntities.GetContext().Hotel.Add(_correntHotel);

            try
            {
                TourBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
      
    }
}
