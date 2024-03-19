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
        private readonly DataManager dictionaryDataManager = new DataManager("../../Data/dictionary.json");
        private readonly DataManager categoryDataManager = new DataManager("../../Data/categories.json");
        public ObservableCollection<Word> Dictionary { get; set; }
        public IList<Category> Categories { get; set; }

        public AddWordForm(ObservableCollection<Word> dictionary)
        {
            InitializeComponent();
            Dictionary = dictionary;
            Categories = categoryDataManager.LoadData<IList<Category>>();
            DataContext = new WordFormViewModel { Categories = Categories };
        }

        private void ButtonAddWordSubmit_Click(object sender, RoutedEventArgs e)
        {
            string name = TextName.Text;
            string definition = TextDefinition.Text;
            string imageUrl = TextImageUrl.Text == "" ? "https://via.placeholder.com/150" : TextImageUrl.Text;
            string categoryName = (ComboBoxCategory.SelectedItem as Category)?.Name ?? ComboBoxCategory.Text;

            if (Dictionary.Any(word => word.Name == name)) { MessageBox.Show($"Word '{name}' already exists!"); return; }
            Category category = Categories.FirstOrDefault(c => c.Name == categoryName) ?? new Category { Id = Categories.Count + 1, Name = categoryName };
            if (!Categories.Contains(category))
            {
                Categories.Add(category);
                categoryDataManager.SaveData(Categories);
            }
            Dictionary.Add(new Word { Id = Dictionary.Count + 1, Name = name, Definition = definition, ImageUrl = imageUrl, Category = category });
            var sortedDictionary = new ObservableCollection<Word>(Dictionary.OrderBy(word => word.Name));
            Dictionary.Clear();
            foreach (Word word in sortedDictionary) Dictionary.Add(word);
            dictionaryDataManager.SaveData(Dictionary);
            Close();
        }
    }
}