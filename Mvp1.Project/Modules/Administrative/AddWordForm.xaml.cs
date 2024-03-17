using Mvp1.Project.Data;
using Mvp1.Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Mvp1.Project.Modules.Administrative
{
    public partial class AddWordForm : Window
    {
        private IList<Word> Dictionary { get; set; }
        public IList<ECategory> Categories { get => Enum.GetValues(typeof(ECategory)).Cast<ECategory>().ToList(); }

        public AddWordForm(IList<Word> Dictionary)
        {
            InitializeComponent();
            this.Dictionary = Dictionary;
            DataContext = this;
        }

        private void ButtonAddWordSubmit_Click(object sender, RoutedEventArgs e)
        {
            string Name = TextName.Text;
            string Definition = TextDefinition.Text;
            string ImageUrl = TextImageUrl.Text == "" ? "https://via.placeholder.com/150" : TextImageUrl.Text;
            ECategory Category = (ECategory)ComboBoxCategory.SelectedItem;

            if (string.IsNullOrWhiteSpace(Name)) MessageBox.Show("Name is required!");
            else if (string.IsNullOrWhiteSpace(Definition)) MessageBox.Show("Definition is required!");
            else if (Dictionary.Any(word => word.Name == Name)) MessageBox.Show($"Word {Name} already exists!");
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
    }
}