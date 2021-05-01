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
    /// Логика взаимодействия для RecomendationsPage.xaml
    /// </summary>
    public partial class RecommendationsPage : Page
    {
        OracleProvider Provider;
        Client Client;
        List<(Contract, ServiceDescription)> ContractServiceList;

        public RecommendationsPage(OracleProvider oracleProvider, Client client)
        {
            InitializeComponent();
            Provider = oracleProvider;
            Client = client;
            FillPage();
        }

        void FillPage()
        {
            ContractServiceList = new List<(Contract, ServiceDescription)>();
            ClientRepository clientRepository = new ClientRepository(Provider);
            ContractRepository contractRepository = new ContractRepository(Provider);

            IEnumerable<TariffPlan> tariffPlans = clientRepository.GetTariffRecommendations(Client.ID, 3);
            TariffRecommendations.Children.Clear();
            TariffRecommendations.Children.Add(GetTitleTariffRow());
            foreach (TariffPlan tariff in tariffPlans)
            {
                TariffRecommendations.Children.Add(GetTariffRow(tariff));
            }
            if (tariffPlans.Count() == 0)
                TariffRecommendations.Children.Add(GetNothingRow());

            IEnumerable<Contract> contracts = clientRepository.GetAllContracts(Client.ID);
            int csCounter = 0;
            for (int i = 0; i < contracts.Count(); i++)
            {
                Contract contract = contracts.ElementAt(i);
                IEnumerable<ServiceDescription> serviceDescriptions = contractRepository.GetServiceRecommendations(contract.ID, 3);
                StackPanel serviceStack = GetServicesStack(contract);
                serviceStack.Children.Add(GetTitleServiceRow());
                for (int j = 0; j < serviceDescriptions.Count(); j++)
                {
                    ServiceDescription serviceDescription = serviceDescriptions.ElementAt(j);
                    var el = (contract, serviceDescription);
                    ContractServiceList.Add(el);
                    serviceStack.Children.Add(GetServiceRow(serviceDescription, csCounter));
                    csCounter++;
                }
                if (serviceDescriptions.Count() == 0)
                    serviceStack.Children.Add(GetNothingRow());
                AllContainer.Children.Add(serviceStack);
            }
        }

        private void CreateContract(object sender, RoutedEventArgs e)
        {
            string myValue = ((Grid)sender).Tag.ToString();
            int tariffIndex = Convert.ToInt32(myValue);
            TariffPlanRepository tariffPlanRepository = new TariffPlanRepository(Provider);
            TariffPlan tariff = tariffPlanRepository.Get(tariffIndex);
            if (tariff != null)
                (Window.GetWindow(this) as MainWindow).OpenExecuteContractPage(tariff);
        }

        private void CreateService(object sender, RoutedEventArgs e)
        {
            string myValue = ((Grid)sender).Tag.ToString();
            int csIndex = Convert.ToInt32(myValue);
            Contract contract = ContractServiceList[csIndex].Item1;
            ServiceDescription serviceDescription = ContractServiceList[csIndex].Item2;
            (Window.GetWindow(this) as MainWindow).OpenExecuteServicePage(contract, serviceDescription);
        }

        Grid GetTitleTariffRow()
        {
            Grid gridRow = new Grid();
            gridRow.Background = new SolidColorBrush(Colors.Gray);
            gridRow.Height = 30;

            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(1, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd1);
            for (int i = 0; i < 2; i++)
            {
                ColumnDefinition cd2 = new ColumnDefinition();
                cd2.Width = new GridLength(3, GridUnitType.Star);
                gridRow.ColumnDefinitions.Add(cd2);
            }

            List<TextBlock> textBlocks = new List<TextBlock>();

            for (int i = 0; i < 3; i++)
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

            textBlocks.ElementAt(0).Text = "Tariff id";
            textBlocks.ElementAt(1).Text = "Tariff name";
            textBlocks.ElementAt(2).Text = "Mounth coast";

            for (int i = 0; i < textBlocks.Count; i++)
            {
                gridRow.Children.Add(textBlocks.ElementAt(i));
            }

            return gridRow;
        }

        Grid GetTariffRow(TariffPlan tariff)
        {
            Grid gridRow = new Grid();
            gridRow.Background = new SolidColorBrush(Colors.LightGray);
            gridRow.Height = 30;
            gridRow.Tag = tariff.ID;
            gridRow.MouseLeftButtonDown += CreateContract;

            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(1, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd1);
            for (int i = 0; i < 2; i++)
            {
                ColumnDefinition cd2 = new ColumnDefinition();
                cd2.Width = new GridLength(3, GridUnitType.Star);
                gridRow.ColumnDefinitions.Add(cd2);
            }

            List<TextBlock> textBlocks = new List<TextBlock>();

            for (int i = 0; i < 3; i++)
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

            textBlocks.ElementAt(0).Text = tariff.ID.ToString();
            textBlocks.ElementAt(1).Text = tariff.TARIFF_NAME;
            textBlocks.ElementAt(2).Text = tariff.TARIFF_AMOUNT.ToString();

            for (int i = 0; i < textBlocks.Count; i++)
            {
                gridRow.Children.Add(textBlocks.ElementAt(i));
            }

            return gridRow;
        }

        StackPanel GetServicesStack(Contract contract)
        {
            StackPanel stackPanel = new StackPanel();

            TariffPlanRepository tariffPlanRepository = new TariffPlanRepository(Provider);
            TariffPlan tariff = tariffPlanRepository.Get(contract.TARIFF_ID);

            TextBlock tb = new TextBlock();
            tb.TextWrapping = TextWrapping.Wrap;
            tb.FontFamily = new FontFamily("Unispace");
            tb.FontSize = 14;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.Text = $"Service recommendations for {contract.ID} ({tariff.TARIFF_NAME}) contract";
            tb.Margin = new Thickness(0, 10, 0, 10);

            stackPanel.Children.Add(tb);

            return stackPanel;
        }

        Grid GetTitleServiceRow()
        {
            Grid gridRow = new Grid();
            gridRow.Background = new SolidColorBrush(Colors.Gray);
            gridRow.Height = 30;

            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(1, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd1);
            ColumnDefinition cd2 = new ColumnDefinition();
            cd2.Width = new GridLength(1, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd2);
            ColumnDefinition cd3 = new ColumnDefinition();
            cd3.Width = new GridLength(6, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd3);

            List<TextBlock> textBlocks = new List<TextBlock>();

            for (int i = 0; i < 3; i++)
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

            textBlocks.ElementAt(0).Text = "Service id";
            textBlocks.ElementAt(1).Text = "Service name";
            textBlocks.ElementAt(2).Text = "Service description";

            for (int i = 0; i < textBlocks.Count; i++)
            {
                gridRow.Children.Add(textBlocks.ElementAt(i));
            }

            return gridRow;
        }

        Grid GetServiceRow(ServiceDescription service, int csIndex)
        {
            Grid gridRow = new Grid();
            gridRow.Background = new SolidColorBrush(Colors.LightGray);
            gridRow.Tag = csIndex;
            gridRow.MouseLeftButtonDown += CreateService;

            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(1, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd1);
            ColumnDefinition cd2 = new ColumnDefinition();
            cd2.Width = new GridLength(1, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd2);
            ColumnDefinition cd3 = new ColumnDefinition();
            cd3.Width = new GridLength(6, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd3);

            List<TextBlock> textBlocks = new List<TextBlock>();

            for (int i = 0; i < 3; i++)
            {
                TextBlock tb = new TextBlock();
                tb.TextWrapping = TextWrapping.Wrap;
                tb.FontFamily = new FontFamily("Unispace");
                tb.FontSize = 12;
                Grid.SetColumn(tb, i);
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.Margin = new Thickness(0, 5, 0, 5);
                textBlocks.Add(tb);
            }

            textBlocks.ElementAt(0).Text = service.ID.ToString();
            textBlocks.ElementAt(1).Text = service.SERVICE_NAME;
            textBlocks.ElementAt(2).Text = service.SERVICE_DESCRIPTION;

            for (int i = 0; i < textBlocks.Count; i++)
            {
                gridRow.Children.Add(textBlocks.ElementAt(i));
            }

            return gridRow;
        }

        Grid GetNothingRow()
        {
            Grid gridRow = new Grid();
            gridRow.Background = new SolidColorBrush(Colors.LightGray);
            gridRow.Height = 30;


            TextBlock tb = new TextBlock();
            tb.TextWrapping = TextWrapping.Wrap;
            tb.FontFamily = new FontFamily("Unispace");
            tb.FontSize = 12;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.Text = "Nothing to recommend";

            gridRow.Children.Add(tb);

            return gridRow;
        }
    }
}
