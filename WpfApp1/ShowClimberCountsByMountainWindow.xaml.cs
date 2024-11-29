using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public partial class ShowClimberCountsByMountainWindow : Window
    {
        public ShowClimberCountsByMountainWindow()
        {
            InitializeComponent();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SQLiteConnection("Data Source=бдкурсач.db"))
                {
                    connection.Open();
                    string query = @"
                    SELECT Mountains.Name AS MountainName, COUNT(DISTINCT GroupClimbers.ClimberId) AS ClimberCount
                    FROM Mountains
                    LEFT JOIN Ascents ON Mountains.Id = Ascents.MountainId
                    LEFT JOIN GroupClimbers ON Ascents.GroupId = GroupClimbers.GroupId
                    GROUP BY Mountains.Name";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            var results = new List<dynamic>();
                            while (reader.Read())
                            {
                                results.Add(new
                                {
                                    MountainName = reader["MountainName"].ToString(),
                                    ClimberCount = Convert.ToInt32(reader["ClimberCount"])
                                });
                            }

                            ClimberCountsDataGrid.ItemsSource = results;
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
