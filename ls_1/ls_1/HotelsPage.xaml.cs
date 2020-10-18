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
    /// Логика взаимодействия для HotelsPage.xaml
    /// </summary>
    public partial class HotelsPage : Page
    {
        public HotelsPage()
        {
            InitializeComponent();


           /* DGridHotels.ItemsSource = TourBaseEntities.GetContext().Hotel.ToList();*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           /* Manager.MainFrame.Navigate(new AddEditPage());*/
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Hotel));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelfotRemove = DGridHotels.SelectedItems.Cast<Hotel>().ToList();
            
            if (MessageBox.Show($" Вы точно хотите удалить {hotelfotRemove.Count()} Элементов ?", " Внимание ", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ToursBaseEntities1.GetContext().Hotel.RemoveRange(hotelfotRemove);
                    ToursBaseEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridHotels.ItemsSource = ToursBaseEntities1.GetContext().Hotel.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ToursBaseEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridHotels.ItemsSource = ToursBaseEntities1.GetContext().Hotel.ToList();
            }
        }
    }
}
