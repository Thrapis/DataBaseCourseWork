using System;
using MobileOperatorApplication.Model;
using MobileOperatorApplication.Oracle;
using MobileOperatorApplication.Pages;
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
using MobileOperatorApplication.Repository;

namespace MobileOperatorApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExecuteService.xaml
    /// </summary>
    public partial class ExecuteServicePage : Page
    {
        OracleProvider Provider;
        Client Client;
        Contract Contract;
        IEnumerable<Contract> Contracts;
        IEnumerable<ServiceDescription> ServiceDescriptions;
        float GeneratedCoast;

        public ExecuteServicePage(OracleProvider oracleProvider, Client client)
        {
            InitializeComponent();
            Provider = oracleProvider;
            Client = client;
            FillContracts();
            FillServices();
        }

        public ExecuteServicePage(OracleProvider oracleProvider, Client client, Contract contract, ServiceDescription serviceDescription)
        {
            InitializeComponent();
            Provider = oracleProvider;
            Client = client;
            FillContracts();
            ContractList.SelectedItem = contract.ID;
            FillServices();
            ServiceList.SelectedItem = serviceDescription.SERVICE_NAME + " (R+)";
        }

        void FillContracts()
        {
            ContractList.Items.Clear();
            ClientRepository clientRepository = new ClientRepository(Provider);
            Contracts = clientRepository.GetAllContracts(Client.ID);
            for (int i = 0; i < Contracts.Count(); i++)
                ContractList.Items.Add(Contracts.ElementAt(i).ID);
            ContractList.SelectedIndex = 0;
        }

        void FillServices()
        {
            ServiceList.Items.Clear();

            ContractRepository contractRepository = new ContractRepository(Provider);
            ServiceDescriptionRepository serviceDescriptionRepository = new ServiceDescriptionRepository(Provider);


            IEnumerable<ServiceDescription> recomended = contractRepository.GetServiceRecommendations(Contract.ID, 3);
            IEnumerable<ServiceDescription> inCase = contractRepository.GetAllServices(Contract.ID)
                .Select(t => serviceDescriptionRepository.Get(t.DESCRIPTION_ID));
            List<ServiceDescription> buf = new List<ServiceDescription>(serviceDescriptionRepository.GetAll());

            for (int i = 0; i < buf.Count(); i++)
            {
                for (int j = 0; j < inCase.Count(); j++)
                {
                    if (buf.ElementAt(i).ID == inCase.ElementAt(j).ID)
                    {
                        buf.Remove(buf.ElementAt(i));
                    }
                }
            }
            ServiceDescriptions = buf;

            for (int i = 0; i < ServiceDescriptions.Count(); i++)
            {
                bool isRecommend = false;
                for (int j = 0; j < recomended.Count(); j++)
                {
                    if (ServiceDescriptions.ElementAt(i).ID == recomended.ElementAt(j).ID)
                    {
                        isRecommend = true;
                        break;
                    }
                }

                if (isRecommend)
                    ServiceList.Items.Add(ServiceDescriptions.ElementAt(i).SERVICE_NAME + " (R+)");
                else
                    ServiceList.Items.Add(ServiceDescriptions.ElementAt(i).SERVICE_NAME);
            }
            ServiceList.SelectedIndex = 0;
        }

        private void Contract_Selected(object sender, RoutedEventArgs e)
        {
            string selected = ContractList.SelectedItem.ToString();
            int contractIndex = Convert.ToInt32(selected);
            for (int i = 0; i < Contracts.Count(); i++)
            {
                if (contractIndex == Contracts.ElementAt(i).ID)
                {
                    Contract = Contracts.ElementAt(i);
                    break;
                }
            }
            TariffPlanRepository tariffPlanRepository = new TariffPlanRepository(Provider);
            TariffPlan tariffPlan = tariffPlanRepository.Get(Contract.TARIFF_ID);
            ToolTip toolTip = new ToolTip();
            TextBlock textBlock = new TextBlock();
            textBlock.Text = tariffPlan.TARIFF_NAME;
            textBlock.TextWrapping = TextWrapping.Wrap;
            toolTip.Content = textBlock;
            ContractList.ToolTip = toolTip;
            FillServices();
        }

        private void Service_Selected(object sender, RoutedEventArgs e)
        {
            ServiceDescription serviceDescription = null;
            if (ServiceList.Items.Count == 0)
                return;
            string selected = ServiceList.SelectedItem.ToString();
            for (int i = 0; i < ServiceDescriptions.Count(); i++)
            {
                if (ServiceDescriptions.ElementAt(i).SERVICE_NAME == selected || ServiceDescriptions.ElementAt(i).SERVICE_NAME + " (R+)" == selected)
                {
                    serviceDescription = ServiceDescriptions.ElementAt(i);
                    break;
                }
            }
            if (serviceDescription != null)
            {
                Random rand = new Random();
                GeneratedCoast = (float)Math.Round(rand.NextDouble() * 5 + 2.5, 2);
                MounthCoast.Text = GeneratedCoast.ToString() + " BYN";
                Description.Text = serviceDescription.SERVICE_DESCRIPTION.Substring(0, 18) + "...";
                ToolTip toolTip = new ToolTip();
                TextBlock textBlock = new TextBlock();
                textBlock.Text = serviceDescription.SERVICE_DESCRIPTION;
                textBlock.TextWrapping = TextWrapping.Wrap;
                toolTip.Content = textBlock;
                Description.ToolTip = toolTip;
            }
        }

        private static string GetRandomPhoneNumber(Random rand)
        {
            string nums = "0123456789";

            string result = "+375";

            for (int i = 0; i < 9; i++)
            {
                result += nums[rand.Next(0, nums.Length)];
            }

            return result;
        }

        void ExecuteService(object sender, RoutedEventArgs e)
        {
            ServiceDescription serviceDescription = null;
            string selected = ServiceList.SelectedItem.ToString();
            for (int i = 0; i < ServiceDescriptions.Count(); i++)
            {
                if (ServiceDescriptions.ElementAt(i).SERVICE_NAME == selected || ServiceDescriptions.ElementAt(i).SERVICE_NAME + " (R+)" == selected)
                {
                    serviceDescription = ServiceDescriptions.ElementAt(i);
                    break;
                }
            }

            ServiceRepository serviceRepository = new ServiceRepository(Provider);
            Service service = new Service(Contract.ID, serviceDescription.ID, GeneratedCoast, DateTime.Now, DateTime.Now.AddMonths(3));
            service.ID = serviceRepository.Insert(service);
            if (service.ID == -1)
            {
                MessageBox.Show("Database internal error", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (service.DESCRIPTION_ID == 1)
                {
                    Random rand = new Random();
                    PhoneNumberRepository phoneNumberRepository = new PhoneNumberRepository(Provider);
                    int temp_insert = 0;
                    while (temp_insert < 2)
                        temp_insert += phoneNumberRepository.Insert(new PhoneNumber(GetRandomPhoneNumber(rand), Contract.ID)) != -1 ? 1 : 0;
                }

                MessageBox.Show($"Execution of service {service.ID} ({serviceDescription.SERVICE_NAME}) was successful", "Message");
                (Window.GetWindow(this) as MainWindow).CloseExecuteServicePage();
            }
        }

        void Close(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).CloseExecuteServicePage();
        }
    }
}
