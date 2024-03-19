using Mvp1.Project.Data;
using Mvp1.Project.Models;
using Mvp1.Project.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Mvp1.Project.Modules.Administrative
{
    public partial class UpdateWordForm : Window
    {
        private readonly DataManager dictionaryDataManager = new DataManager("../../Data/dictionary.json");
        private readonly DataManager categoryDataManager = new DataManager("../../Data/categories.json");

        public Word Word { get; set; }
        public ObservableCollection<Word> Dictionary { get; set; }
        public IList<Category> Categories { get; set; }

        public UpdateWordForm(Word word, ObservableCollection<Word> dictionary)
        {
            InitializeComponent();
            Word = word;
            Dictionary = dictionary;
            Categories = categoryDataManager.LoadData<IList<Category>>();
            DataContext = new WordFormViewModel { Categories = Categories };

            TextName.Text = Word.Name;
            TextDefinition.Text = Word.Definition;
            TextImageUrl.Text = Word.ImageUrl;
            ComboBoxCategory.Text = Word.Category.Name;
        }

        private void ButtonUpdateWordSubmit_Click(object sender, RoutedEventArgs e)
        {
            string name = TextName.Text;
            string definition = TextDefinition.Text;
            string imageUrl = TextImageUrl.Text == "" ? "https://via.placeholder.com/150" : TextImageUrl.Text;
            string categoryName = (ComboBoxCategory.SelectedItem as Category)?.Name ?? ComboBoxCategory.Text;

            if (Dictionary.Any(word => word.Name == name && word.Id != Word.Id)) { MessageBox.Show($"Word '{name}' already exists!"); return; }
            Category category = Categories.FirstOrDefault(c => c.Name == categoryName) ?? new Category { Id = Categories.Count + 1, Name = categoryName };
            if (!Categories.Contains(category))
            {
                Categories.Add(category);
                categoryDataManager.SaveData(Categories);
            }
            Word wordToUpdate = Dictionary.First(word => word.Id == Word.Id);
            wordToUpdate.Name = name;
            wordToUpdate.Definition = definition;
            wordToUpdate.ImageUrl = imageUrl;
            wordToUpdate.Category = category;
            var sortedDictionary = new ObservableCollection<Word>(Dictionary.OrderBy(word => word.Name));
            Dictionary.Clear();
            foreach (Word word in sortedDictionary) Dictionary.Add(word);
            dictionaryDataManager.SaveData(Dictionary);
            Close();
        }
    }
}