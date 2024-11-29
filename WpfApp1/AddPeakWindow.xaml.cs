using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1 { 
}
public partial class AddPeakWindow : Window
{
    public object PeakNameTextBox { get; private set; }
    public object CountryTextBox { get; private set; }
    public object HeightTextBox { get; private set; }

    public AddPeakWindow() => InitializeComponent();

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }

    private void AddPeakButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string peakName = (string)PeakNameTextBox;
            string country = (string)CountryTextBox;
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=бдкурсач.db"))
            {
                conn.Open();
                string query = "INSERT INTO Горы (Название, Страна, Высота) VALUES (@Name, @Country, @Height)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", peakName);
                    cmd.Parameters.AddWithValue("@Country", country);
                    int height = int.Parse((string)HeightTextBox);
                    cmd.Parameters.AddWithValue("@Height", height);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Вершина успешно добавлена!");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}");
        }
    }
}