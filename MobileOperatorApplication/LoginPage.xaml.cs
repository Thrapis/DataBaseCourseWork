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
using Transport_Assistant.Data;

namespace Transport_Assistant
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        void EnterApplication(object sender, EventArgs e)
        {
            bool Access = false;
            Account account = null;
            if (sender == EnterButton || (e as KeyEventArgs).Key == Key.Enter)
            {
                Message.Text = "";
                using (var context = new LoginDBContext())
                {
                    foreach (var elem in context.Accounts)
                    {
                        if (elem.Login == Login.Text)
                        {
                            if (SaltedHash.Verify(elem.Salt, elem.Hash, Password.Password))
                            {
                                account = elem;
                                Access = true;
                                break; 
                            }  
                        }
                    }
                }
                if (Access)
                    (Window.GetWindow(this) as MainWindow).CloseLoginPage(account);
                else
                    Message.Text = "Неверный логин или пароль";
            }
        }
        void RegistrationPage(object sender, EventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).OpenRegistrationPage();
        }
    }
}
