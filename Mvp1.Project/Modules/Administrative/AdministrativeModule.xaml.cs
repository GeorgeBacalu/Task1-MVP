using Mvp1.Project.Data;
using Mvp1.Project.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Mvp1.Project.Modules.Administrative
{
    public partial class AdministrativeModule : Window
    {
        public IList<User> Users { get; private set; }

        public AdministrativeModule()
        {
            InitializeComponent();
            DataManager dataManager = new DataManager("../../Data/users.json");
            Users = dataManager.LoadData<IList<User>>();
        }

        private void ButtonAdminLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = TextUsername.Text;
            string password = TextPassword.Password;
            User user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null) MessageBox.Show("Invalid username or password!");
            else if (user.Role == ERole.User) MessageBox.Show("You don't have permission to access this module!");
            else new DictionaryManager().Show();
        }
    }
}