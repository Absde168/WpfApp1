using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public partial class ShowAscentsByDateWindow : Window
    {
        public ShowAscentsByDateWindow()
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
                    SELECT Ascents.AscentDate, Groups.Name AS GroupName, Mountains.Name AS MountainName 
                    FROM Ascents
                    INNER JOIN Groups ON Ascents.GroupId = Groups.Id
                    INNER JOIN Mountains ON Ascents.MountainId = Mountains.Id
                    WHERE Ascents.AscentDate BETWEEN @StartDate AND @EndDate";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        using (var reader = command.ExecuteReader())
                        {
                            var ascents = new List<dynamic>();
                            while (reader.Read())
                            {
                                ascents.Add(new
                                {
                                    AscentDate = Convert.ToDateTime(reader["AscentDate"]).ToString("yyyy-MM-dd"),
                                    GroupName = reader["GroupName"].ToString(),
                                    MountainName = reader["MountainName"].ToString()
                                });
                            }

                            AscentsDataGrid.ItemsSource = ascents;
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

