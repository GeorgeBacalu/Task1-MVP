using Mvp1.Project.Data;
using Mvp1.Project.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Mvp1.Project.Modules.WordFinder
{
    public partial class WordFinderModule : Window
    {
        public IList<User> Users { get; private set; }

        public WordFinderModule()
        {
            InitializeComponent();
            DataManager dataManager = new DataManager("../../Data/users.json");
            Users = dataManager.LoadData<IList<User>>();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = TextUsername.Text;
            string password = TextPassword.Password;
            User user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null) MessageBox.Show("Invalid username or password!");
            else new WordFinderDashboard().Show();
        }
    }
}