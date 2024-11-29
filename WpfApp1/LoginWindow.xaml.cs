using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{

}
public partial class LoginWindow : Window
{
    public object LoginTextBox { get; private set; }

    public LoginWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e, string password)
    {
        // Получение введенных данных
        string login = (string)LoginTextBox;

        // Простейшая проверка логина и пароля
        if (login == "admin" && password == "1234") // Замените на свою проверку
        {
            MessageBox.Show("Добро пожаловать!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            // Переход к главному окну
            LoginWindow mainWindow = new LoginWindow();
            mainWindow.Show();
            this.Close();
        }
        else
        {
            MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        // Закрытие окна
        this.Close();
    }
}

