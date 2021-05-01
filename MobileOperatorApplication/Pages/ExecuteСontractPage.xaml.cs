using System;
using MobileOperatorApplication.Model;
using MobileOperatorApplication.Oracle;
using MobileOperatorApplication.Repository;
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
using System.Text.RegularExpressions;

namespace MobileOperatorApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExecuteСontractPage.xaml
    /// </summary>
    public partial class ExecuteСontractPage : Page
    {
        OracleProvider Provider;
        Client Client;
        IEnumerable<TariffPlan> TariffPlans;
        bool PhoneValide = false;

        public ExecuteСontractPage(OracleProvider oracleProvider, Client client)
        {
            InitializeComponent();
            Provider = oracleProvider;
            Client = client;
            FillTariffPlans();
            TariffPlan.SelectedIndex = 0;
        }

        public ExecuteСontractPage(OracleProvider oracleProvider, Client client, TariffPlan tariffPlan)
        {
            InitializeComponent();
            Provider = oracleProvider;
            Client = client;
            FillTariffPlans();
            TariffPlan.SelectedItem = tariffPlan.TARIFF_NAME + " (R+)";
        }

        void FillTariffPlans()
        {
            ClientRepository clientRepository = new ClientRepository(Provider);
            TariffPlanRepository tariffPlanRepository = new TariffPlanRepository(Provider);

            IEnumerable<TariffPlan> recomended = clientRepository.GetTariffRecommendations(Client.ID, 3);
            TariffPlans = tariffPlanRepository.GetAll();

            for (int i = 0; i < TariffPlans.Count(); i++)
            {
                bool isRecommend = false;
                for (int j = 0; j < recomended.Count(); j++)
                {
                    if (TariffPlans.ElementAt(i).ID == recomended.ElementAt(j).ID)
                    {
                        isRecommend = true;
                        break;
                    }
                }

                if (isRecommend)
                    TariffPlan.Items.Add(TariffPlans.ElementAt(i).TARIFF_NAME + " (R+)");
                else
                    TariffPlan.Items.Add(TariffPlans.ElementAt(i).TARIFF_NAME);
            }
        }

        private void TariffPlan_Selected(object sender, RoutedEventArgs e)
        {
            TariffPlan tariff = null;
            string selected = TariffPlan.SelectedItem.ToString();
            for (int i = 0; i < TariffPlans.Count(); i++)
            {
                if (TariffPlans.ElementAt(i).TARIFF_NAME == selected || TariffPlans.ElementAt(i).TARIFF_NAME + " (R+)" == selected)
                {
                    tariff = TariffPlans.ElementAt(i);
                    break;
                }
            }
            if (tariff != null)
                TariffInfo.Text = tariff.TARIFF_AMOUNT.ToString() + " BYN";
        }

        private void PhoneNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"([0-9]|\+)");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void PhoneNumberValide(object sender, RoutedEventArgs e)
        {
            string number = DesiredPN.Text;
            Regex regex = new Regex(@"^(\+375|80)(\d{2})(\d{3})(\d{2})(\d{2})$");
            bool valide = regex.IsMatch(number);
            if (!valide)
            {
                Message.Text = "Incorrect phone format. Requires BY phone format";
                PhoneValide = false;
                return;
            }
            PhoneNumberRepository phoneNumberRepository = new PhoneNumberRepository(Provider);
            PhoneNumber phoneNumber = phoneNumberRepository.Get(number);
            if (phoneNumber == null)
            {
                Message.Text = "";
                PhoneValide = true;
            }
            else
            {
                Message.Text = "The phone number is already in use";
                PhoneValide = false;
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

        void ExecuteContract(object sender, RoutedEventArgs e)
        {
            TariffPlan tariff = null;
            string selected = TariffPlan.SelectedItem.ToString();
            for (int i = 0; i < TariffPlans.Count(); i++)
            {
                if (TariffPlans.ElementAt(i).TARIFF_NAME == selected || TariffPlans.ElementAt(i).TARIFF_NAME + " (R+)" == selected)
                {
                    tariff = TariffPlans.ElementAt(i);
                    break;
                }
            }

            Random rand = new Random();

            EmployeeRepository employeeRepository = new EmployeeRepository(Provider);
            IEnumerable<Employee> employees = employeeRepository.GetAll();
            Employee employee = employees.ElementAt(rand.Next(0, employees.Count()));

            ContractRepository contractRepository = new ContractRepository(Provider);
            PhoneNumberRepository phoneNumberRepository = new PhoneNumberRepository(Provider);

            string number = "";
            if (PhoneValide)
            {
                number = DesiredPN.Text;
            }
            else
            {
                number = GetRandomPhoneNumber(rand);
                
                PhoneNumber phoneNumber = phoneNumberRepository.Get(number);
                while (phoneNumber != null)
                {
                    number = GetRandomPhoneNumber(rand);
                    phoneNumber = phoneNumberRepository.Get(number);
                }

                Message.Text = "";
                DesiredPN.Text = number;
            }

            Contract contract = new Contract(tariff.ID, Client.ID, employee.ID, DateTime.Now);
            contract.ID = contractRepository.Insert(contract);
            if (contract.ID == -1)
            {
                MessageBox.Show("Database internal error", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            PhoneNumber newPhoneNumber = new PhoneNumber(number, contract.ID);
            newPhoneNumber.ID = phoneNumberRepository.Insert(newPhoneNumber);
            if (newPhoneNumber.ID == -1)
            {
                contractRepository.Delete(contract.ID);
                MessageBox.Show("Database internal error", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show($"Execution of contract {contract.ID} ({tariff.TARIFF_NAME}) with" +
               $" phone number {newPhoneNumber.PHONE_NUMBER} was successful. Employee {employee.FULL_NAME}" +
               $" signed it", "Message");
                (Window.GetWindow(this) as MainWindow).CloseExecuteContractPage();
            }
        }

        void Close(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).CloseExecuteContractPage();
        }
    }
}
