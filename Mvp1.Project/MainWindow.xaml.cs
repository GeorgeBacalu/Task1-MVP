using Mvp1.Project.Modules.Administrative;
using Mvp1.Project.Modules.WordFinder;
using Mvp1.Project.Modules.Entertainment;
using System.Windows;

namespace Mvp1.Project
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void ButtonAdministrative_Click(object sender, RoutedEventArgs e) => new AdministrativeModule().Show();

        private void ButtonWordFinder_Click(object sender, RoutedEventArgs e) => new WordFinderModule().Show();

        private void ButtonEntertainment_Click(object sender, RoutedEventArgs e) => new EntertainmentModule().Show();
    }
}