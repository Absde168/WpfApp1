using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public partial class AscentsWindow : Window
    {
        public AscentsWindow()
        {
            InitializeComponent();
            LoadAscents();
        }

        private void LoadAscents()
        {
            try
            {
                using (var connection = new SQLiteConnection("Data Source=бдкурсач.db"))
                {
                    connection.Open();
                    string query = @"
                    SELECT 
                        Mountains.Name AS PeakName,
                        Groups.Name AS GroupName,
                        Ascents.AscentDate 
                    FROM Ascents
                    INNER JOIN Groups ON Ascents.GroupId = Groups.Id
                    INNER JOIN Mountains ON Ascents.MountainId = Mountains.Id
                    ORDER BY Ascents.AscentDate";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            var ascents = new List<dynamic>();
                            while (reader.Read())
                            {
                                ascents.Add(new
                                {
                                    PeakName = reader["PeakName"].ToString(),
                                    GroupName = reader["GroupName"].ToString(),
                                    AscentDate = Convert.ToDateTime(reader["AscentDate"])
                                });
                            }

                            AscentsDataGrid.ItemsSource = ascents;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

