using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1 
{ 
 public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ShowAscents_Click(object sender, RoutedEventArgs e)
    {
        // Открыть окно для отображения восхождений
    }

    private void ShowClimbers_Click(object sender, RoutedEventArgs e)
    {
        // Открыть окно для отображения альпинистов
    }

    private void QueryGroupsByMountain_Click(object sender, RoutedEventArgs e)
    {
        // Открыть окно с запросом "Группы по горе"
    }

    private void AddMountain_Click(object sender, RoutedEventArgs e)
    {
        // Открыть окно для добавления новой вершины
    }
}
}