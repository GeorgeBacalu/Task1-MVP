using Mvp1.Project.Data;
using Mvp1.Project.Models;
using Mvp1.Project.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Mvp1.Project.Modules.Administrative
{
    public partial class AddWordForm : Window
    {
        private ObservableCollection<Word> Dictionary { get; set; }

        public AddWordForm(ObservableCollection<Word> Dictionary)
        {
            InitializeComponent();
            this.Dictionary = Dictionary;
        }

        private void ButtonAddWordSubmit_Click(object sender, RoutedEventArgs e)
        {
            string name = TextName.Text;
            string definition = TextDefinition.Text;
            string imageUrl = TextImageUrl.Text == "" ? "https://via.placeholder.com/150" : TextImageUrl.Text;
            ECategory category = (ECategory)ComboBoxCategory.SelectedItem;

            if (Dictionary.Any(word => word.Name == name)) MessageBox.Show($"Word {name} already exists!");
            else
            {
                Dictionary.Add(new Word { Id = Dictionary.Count + 1, Name = name, Definition = definition, ImageUrl = imageUrl, Category = category });
                var sortedDictionary = new ObservableCollection<Word>(Dictionary.OrderBy(word => word.Name));
                Dictionary.Clear();
                foreach (var word in sortedDictionary) Dictionary.Add(word);
                new DataManager("../../Data/dictionary.json").SaveData(Dictionary);
                Close();
            }
        }
    }
}