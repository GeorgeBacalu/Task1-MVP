using Mvp1.Project.Data;
using Mvp1.Project.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Mvp1.Project.Modules.Administrative
{
    public partial class UpdateWordForm : Window
    {
        public Word Word { get; set; }
        public ObservableCollection<Word> Dictionary { get; set; }

        public UpdateWordForm(Word Word, ObservableCollection<Word> Dictionary)
        {
            InitializeComponent();
            this.Word = Word;
            this.Dictionary = Dictionary;
            TextName.Text = Word.Name;
            TextDefinition.Text = Word.Definition;
            TextImageUrl.Text = Word.ImageUrl;
            ComboBoxCategory.SelectedItem = Word.Category;
        }

        private void ButtonUpdateWordSubmit_Click(object sender, RoutedEventArgs e)
        {
            string name = TextName.Text;
            string definition = TextDefinition.Text;
            string imageUrl = TextImageUrl.Text == "" ? "https://via.placeholder.com/150" : TextImageUrl.Text;
            ECategory category = (ECategory)ComboBoxCategory.SelectedItem;

            if (Dictionary.Any(word => word.Name == name && word.Id != Word.Id)) MessageBox.Show($"Word {name} already exists!");
            else
            {
                Word wordToUpdate = Dictionary.First(word => word.Id == Word.Id);
                wordToUpdate.Name = name;
                wordToUpdate.Definition = definition;
                wordToUpdate.ImageUrl = imageUrl;
                wordToUpdate.Category = category;
                var sortedDictionary = new ObservableCollection<Word>(Dictionary.OrderBy(word => word.Name));
                Dictionary.Clear();
                foreach (Word word in sortedDictionary) Dictionary.Add(word);
                new DataManager("../../Data/dictionary.json").SaveData(Dictionary);
                Close();
            }
        }
    }
}