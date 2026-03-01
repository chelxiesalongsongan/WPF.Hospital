using System;
using System.Windows;
using WPF.Hospital.Service;

namespace WPF.Hospital
{
    public partial class DeleteHistory : Window
    {
        private readonly IHistoryService _historyService;

        public DeleteHistory(IHistoryService historyService)
        {
            InitializeComponent();
            _historyService = historyService;
        }

        private void DeleteHistory_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbHistoryId.Text))
            {
                MessageBox.Show("Please enter a History ID.");
                return;
            }

            if (!int.TryParse(tbHistoryId.Text, out int historyId))
            {
                MessageBox.Show("History ID must be a number.");
                return;
            }

            try
            {
                var result = _historyService.Delete(historyId);
                if (result.Ok)
                {
                    MessageBox.Show("History deleted successfully!");
                    Close();
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting history: {ex.Message}");
            }
        }
    }
}
