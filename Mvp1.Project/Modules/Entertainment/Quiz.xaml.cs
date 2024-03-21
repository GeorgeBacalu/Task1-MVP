using Mvp1.Project.ViewModels;
using System.Windows;

namespace Mvp1.Project.Modules.Entertainment
{
    public partial class Quiz : Window
    {
        public Quiz()
        {
            InitializeComponent();
            DataContext = new QuizViewModel();
        }
    }
}