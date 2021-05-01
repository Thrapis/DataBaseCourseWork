using MobileOperatorApplication.Model;
using MobileOperatorApplication.Oracle;
using MobileOperatorApplication.Repository;
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

namespace MobileOperatorApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        OracleProvider Provider;
        Client Client;
        IEnumerable<Service> Services;

        public ServicesPage(OracleProvider oracleProvider, Client client)
        {
            InitializeComponent();
            Provider = oracleProvider;
            Client = client;
            FillWithInfo(null, null);
        }

        void FillWithInfo(object sender, EventArgs e)
        {
            ClientRepository clientRepository = new ClientRepository(Provider);

            Services = clientRepository.GetAllServices(Client.ID);

            ServicesStack.Children.Clear();

            for (int i = 0; i < Services.Count(); i++)
            {
                Service service = Services.ElementAt(i);
                ServicesStack.Children.Add(GetServiceRow(service, i));
            }
        }

        void DeleteService(object sender, RoutedEventArgs e)
        {
            string myValue = ((MenuItem)sender).Tag.ToString();
            int index = Convert.ToInt32(myValue);
            Service service = Services.ElementAt(index);

            ServiceRepository serviceRepository = new ServiceRepository(Provider);

            MessageBoxResult result = MessageBox.Show($"Are you sure you want to terminate {service.ID} service?", "Attention", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (service.DESCRIPTION_ID == 1)
                {
                    ContractRepository contractRepository = new ContractRepository(Provider);
                    IEnumerable<PhoneNumber> phoneNumbers = contractRepository.GetAllPhoneNumbers(service.CONTRACT_ID);
                    SortedList<int, PhoneNumber> sortedPhoneNumbers = new SortedList<int, PhoneNumber>();
                    for (int i = 0; i < phoneNumbers.Count(); i++)
                    {
                        sortedPhoneNumbers.Add(phoneNumbers.ElementAt(i).ID, phoneNumbers.ElementAt(i));
                    }
                    PhoneNumberRepository phoneNumberRepository = new PhoneNumberRepository(Provider);
                    for (int i = sortedPhoneNumbers.Count - 1; i >= sortedPhoneNumbers.Count - 3; i--)
                    {
                        phoneNumberRepository.Delete(sortedPhoneNumbers.ElementAt(i).Value.ID);
                    }
                }

                int deleted = serviceRepository.Delete(service.ID);
                if (deleted == -1)
                    MessageBox.Show("Database internal error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    MessageBox.Show($"Service {service.ID} was terminated");

            }

            FillWithInfo(null, null);
        }

        private void CreateService(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).OpenExecuteServicePage();
        }

        Grid GetServiceRow(Service service, int index)
        {
            ServiceDescriptionRepository serviceDescriptionRepository = new ServiceDescriptionRepository(Provider);

            ServiceDescription serviceDescription = serviceDescriptionRepository.Get(service.DESCRIPTION_ID);

            Grid gridRow = new Grid();
            gridRow.Background = new SolidColorBrush(Colors.LightGray);

            ContextMenu context = new ContextMenu();
            MenuItem delete = new MenuItem();
            delete.Header = "Terminate";
            delete.Tag = index;
            delete.Click += DeleteService;
            context.Items.Add(delete);
            gridRow.ContextMenu = context;

            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(0.5, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd1);
            for (int i = 0; i < 5; i++)
            {
                ColumnDefinition cd2 = new ColumnDefinition();
                cd2.Width = new GridLength(1, GridUnitType.Star);
                gridRow.ColumnDefinitions.Add(cd2);
            }

            List<TextBlock> textBlocks = new List<TextBlock>();

            for (int i = 0; i < 6; i++)
            {
                TextBlock tb = new TextBlock();
                tb.TextWrapping = TextWrapping.Wrap;
                tb.FontFamily = new FontFamily("Unispace");
                tb.FontSize = 12;
                Grid.SetColumn(tb, i);
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Center;
                textBlocks.Add(tb);
            }
            ToolTip toolTip = new ToolTip();
            TextBlock descr = new TextBlock();
            descr.Text = serviceDescription.SERVICE_DESCRIPTION;
            descr.TextWrapping = TextWrapping.Wrap;
            toolTip.Content = descr;

            textBlocks.ElementAt(0).Text = service.ID.ToString();
            textBlocks.ElementAt(1).Text = service.CONTRACT_ID.ToString();
            textBlocks.ElementAt(2).Text = serviceDescription.SERVICE_NAME;
            textBlocks.ElementAt(2).ToolTip = toolTip;
            textBlocks.ElementAt(3).Text = service.SERVICE_AMOUNT.ToString() + " BYN";
            textBlocks.ElementAt(4).Text = service.CONNECTION_DATE.ToString();
            textBlocks.ElementAt(5).Text = service.DISCONNECTION_DATE.ToString();

            for (int i = 0; i < textBlocks.Count; i++)
            {
                gridRow.Children.Add(textBlocks.ElementAt(i));
            }

            return gridRow;
        }
    }
}
