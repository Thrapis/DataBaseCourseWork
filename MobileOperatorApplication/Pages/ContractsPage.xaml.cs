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
    /// Логика взаимодействия для ContractsPage.xaml
    /// </summary>
    public partial class ContractsPage : Page
    {
        OracleProvider Provider;
        Client Client;
        IEnumerable<Contract> Contracts;

        public ContractsPage(OracleProvider oracleProvider, Client client)
        {
            InitializeComponent();
            Provider = oracleProvider;
            Client = client;
            FillWithInfo(null, null);
        }

        void FillWithInfo(object sender, EventArgs e)
        {
            ClientRepository clientRepository = new ClientRepository(Provider);

            Contracts = clientRepository.GetAllContracts(Client.ID);

            ContractsStack.Children.Clear();

            for (int i = 0; i < Contracts.Count(); i++)
            {
                ContractsStack.Children.Add(GetContractRow(Contracts.ElementAt(i), i));
            }
        }

        void PayForContract(object sender, RoutedEventArgs e)
        {
            string myValue = ((MenuItem)sender).Tag.ToString();
            int index = Convert.ToInt32(myValue);

            Contract contract = Contracts.ElementAt(index);

            (Window.GetWindow(this) as MainWindow).OpenPaymentForContractPage(contract);
        }

        void DeleteContract(object sender, RoutedEventArgs e)
        {
            string myValue = ((MenuItem)sender).Tag.ToString();
            int index = Convert.ToInt32(myValue);

            Contract contract = Contracts.ElementAt(index);
            ContractRepository contractRepository = new ContractRepository(Provider);
            float balance = contractRepository.GetContractBalance(contract.ID);

            if (balance < 0)
            {
                MessageBox.Show($"Balance of your {contract.ID} contract is under 0 BYN. Please pay for you debt", "Attention");
                return;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to terminate {contract.ID} contract?", "Attention", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    int deleted = contractRepository.Delete(contract.ID);
                    Console.WriteLine(deleted);
                    FillWithInfo(null, null);
                    if (deleted == -1)
                        MessageBox.Show("Database internal error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else 
                        MessageBox.Show($"Contract {contract.ID} was terminated");
                }
            }
        }

        private void CreateContract(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).OpenExecuteContractPage();
        }

        Grid GetContractRow(Contract contract, int index)
        {
            ContractRepository contractRepository = new ContractRepository(Provider);
            EmployeeRepository employeeRepository = new EmployeeRepository(Provider);
            TariffPlanRepository tariffRepository = new TariffPlanRepository(Provider);

            TariffPlan tariff = tariffRepository.Get(contract.TARIFF_ID);
            Employee employee = employeeRepository.Get(contract.EMPLOYEE_ID);
            float balance = contractRepository.GetContractBalance(contract.ID);

            Grid gridRow = new Grid();
            gridRow.Background = new SolidColorBrush(Colors.LightGray);

            ContextMenu context = new ContextMenu();
            MenuItem pay = new MenuItem();
            pay.Header = "Payment";
            pay.Tag = index;
            pay.Click += PayForContract;
            MenuItem delete = new MenuItem();
            delete.Header = "Terminate";
            delete.Tag = index;
            delete.Click += DeleteContract;
            context.Items.Add(pay);
            context.Items.Add(delete);
            gridRow.ContextMenu = context;

            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(0.5, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd1);
            for (int i = 0; i < 6; i++)
            {
                ColumnDefinition cd2 = new ColumnDefinition();
                cd2.Width = new GridLength(1, GridUnitType.Star);
                gridRow.ColumnDefinitions.Add(cd2);
            }

            List<TextBlock> textBlocks = new List<TextBlock>();

            for (int i = 0; i < 7; i++)
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

            textBlocks.ElementAt(0).Text = contract.ID.ToString();
            textBlocks.ElementAt(1).Text = tariff.TARIFF_NAME;
            textBlocks.ElementAt(2).Text = Client.FULL_NAME;
            textBlocks.ElementAt(3).Text = employee.FULL_NAME;
            textBlocks.ElementAt(4).Text = contract.SIGNING_DATETIME.ToString();
            textBlocks.ElementAt(5).Text = tariff.TARIFF_AMOUNT.ToString();
            textBlocks.ElementAt(6).Text = balance.ToString();

            for (int i = 0; i < textBlocks.Count; i++)
            {
                gridRow.Children.Add(textBlocks.ElementAt(i));
            }

            return gridRow;
        }
    }
}
