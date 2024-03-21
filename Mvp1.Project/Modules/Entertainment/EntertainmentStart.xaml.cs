using System.Windows;

namespace Mvp1.Project.Modules.Entertainment
{
    public partial class EntertainmentStart : Window
    {
        public EntertainmentStart()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e) => new Quiz().Show();
    }
}