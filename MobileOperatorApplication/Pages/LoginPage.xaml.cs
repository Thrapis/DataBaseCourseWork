using MobileOperatorApplication.Model;
using MobileOperatorApplication.Oracle;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        OracleProvider provider;

        public LoginPage(OracleProvider oracleProvider)
        {
            InitializeComponent();
            provider = oracleProvider;
        }

        void EnterApplication(object sender, EventArgs e)
        {
            AccountInfo account = provider.GetAccount(Login.Text, Password.Password);

            if (account != null)
                (Window.GetWindow(this) as MainWindow).CloseLoginPage(account);
            else
                Message.Text = "Неверный логин или пароль";
        }
    }
}
