using System.Windows;

namespace Mvp1.Project.Modules.Entertainment
{
    public partial class ResultsScreen : Window
    {
        public ResultsScreen()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) => new EntertainmentModule().Show();

        private void PlayAgainButton_Click(object sender, RoutedEventArgs e) => new EntertainmentStart().Show();
    }
}