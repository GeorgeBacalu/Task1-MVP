using Mvp1.Project.Data;
using Mvp1.Project.Models;
using Mvp1.Project.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Mvp1.Project.Modules.Administrative
{
    public partial class AddWordForm : Window
    {
        private IList<Word> Dictionary { get; set; }

        public AddWordForm(IList<Word> Dictionary)
        {
            InitializeComponent();
            this.Dictionary = Dictionary;
            DataContext = new WordFormViewModel();
        }

        private void ButtonAddWordSubmit_Click(object sender, RoutedEventArgs e)
        {
            var wordFormViewModel = DataContext as WordFormViewModel;
            if (wordFormViewModel != null && wordFormViewModel.IsValid)
            {
                string Name = TextName.Text;
                string Definition = TextDefinition.Text;
                string ImageUrl = TextImageUrl.Text == "" ? "https://via.placeholder.com/150" : TextImageUrl.Text;
                ECategory Category = (ECategory)ComboBoxCategory.SelectedItem;

                if (Dictionary.Any(word => word.Name == Name)) MessageBox.Show($"Word {Name} already exists!");
                else
                {
                    Dictionary.Add(new Word { Id = Dictionary.Count + 1, Name = Name, Definition = Definition, ImageUrl = ImageUrl, Category = Category });
                    var sortedDictionary = Dictionary.OrderBy(word => word.Name);
                    Dictionary = new ObservableCollection<Word>(sortedDictionary);
                    new DataManager("../../Data/dictionary.json").SaveData(Dictionary);
                    MessageBox.Show("Word added successfully!");
                    Close();
                }
            }
            else MessageBox.Show("Please fill in all required fields!");
        }
    }
}