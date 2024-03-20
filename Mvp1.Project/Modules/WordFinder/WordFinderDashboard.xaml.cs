using Mvp1.Project.ViewModels;
using System.Windows;

namespace Mvp1.Project.Modules.WordFinder
{
    public partial class WordFinderDashboard : Window
    {
        public WordFinderDashboard()
        {
            InitializeComponent();
            DataContext = new WordFinderViewModel();
        }
    }
}