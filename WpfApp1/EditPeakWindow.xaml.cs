using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public partial class EditPeakWindow : Window
    {
        public EditPeakWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string peakName = PeakNameTextBox.Text;
                string newCountry = NewCountryTextBox.Text;
                int newHeight = int.Parse(NewHeightTextBox.Text);

                using (var connection = new SQLiteConnection("Data Source=бдкурсач.db"))
                {
                    connection.Open();

                    string checkQuery = @"
                    SELECT COUNT(*) 
                    FROM Ascents 
                    INNER JOIN Mountains ON Ascents.MountainId = Mountains.Id
                    WHERE Mountains.Name = @PeakName";

                    using (var checkCommand = new SQLiteCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@PeakName", peakName);
                        int ascentCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (ascentCount > 0)
                        {
                            MessageBox.Show("На эту вершину уже были восхождения. Изменение невозможно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                    string updateQuery = @"
                    UPDATE Mountains
                    SET Height = @NewHeight, Country = @NewCountry
                    WHERE Name = @PeakName";

                    using (var updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@NewHeight", newHeight);
                        updateCommand.Parameters.AddWithValue("@NewCountry", newCountry);
                        updateCommand.Parameters.AddWithValue("@PeakName", peakName);

                        int rowsUpdated = updateCommand.ExecuteNonQuery();
                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Данные о вершине успешно обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Вершина не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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
