using MobileOperatorApplication.Model;
using MobileOperatorApplication.Oracle;
using MobileOperatorApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для PaymentForContractPage.xaml
    /// </summary>
    public partial class PaymentForContractPage : Page
    {
        OracleProvider Provider;
        Contract Contract;

        public PaymentForContractPage(OracleProvider oracleProvider, Contract contract)
        {
            InitializeComponent();
            Provider = oracleProvider;
            Contract = contract;

            ContractId.Text = Contract.ID.ToString();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Pay(object sender, RoutedEventArgs e)
        {
            if (sender != EnterButton && (e as KeyEventArgs).Key != Key.Enter)
                return;

            try
            {
                float value = (float)Convert.ToDouble(Value.Text);

                if (value <= 0 || CardNumber.Text.Length != 16)
                    throw new Exception();

                PaymentRepository paymentRepository = new PaymentRepository(Provider);

                Payment payment = new Payment(Contract.ID, value, DateTime.Now);

                int success = paymentRepository.Insert(payment);

                if (success != -1)
                {
                    MessageBox.Show($"Pament {value} BYN for contract {Contract.ID} was successful");
                    (Window.GetWindow(this) as MainWindow).ClosePaymentForContractPage();
                }
                else throw new Exception();
            }
            catch
            {
                MessageBox.Show($"Payment error");
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).ClosePaymentForContractPage();
        }
    }
}
