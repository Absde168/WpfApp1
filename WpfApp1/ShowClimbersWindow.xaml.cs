using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace WpfApp1
{
    public partial class ShowClimbersWindow : Window
    {
        public ShowClimbersWindow()
        {
            InitializeComponent();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime startDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
                DateTime endDate = EndDatePicker.SelectedDate ?? DateTime.MaxValue;

                using (var connection = new SQLiteConnection("Data Source=бдкурсач.db"))
                {
                    connection.Open();
                    string query = @"
                    SELECT Climbers.Name, Climbers.Address 
                    FROM Climbers
                    INNER JOIN GroupClimbers ON Climbers.Id = GroupClimbers.ClimberId
                    INNER JOIN Ascents ON GroupClimbers.GroupId = Ascents.GroupId
                    WHERE Ascents.AscentDate BETWEEN @StartDate AND @EndDate";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        using (var reader = command.ExecuteReader())
                        {
                            var climbers = new List<dynamic>();
                            while (reader.Read())
                            {
                                climbers.Add(new
                                {
                                    Name = reader["Name"].ToString(),
                                    Address = reader["Address"].ToString()
                                });
                            }

                            ClimbersDataGrid.ItemsSource = climbers;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
