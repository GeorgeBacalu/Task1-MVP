using Mvp1.Project.Data;
using Mvp1.Project.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mvp1.Project.Modules.Administrative
{
    public partial class DictionaryManager : Window
    {
        public ObservableCollection<Word> Dictionary { get; set; }

        public DictionaryManager()
        {
            InitializeComponent();
            DataManager dataManager = new DataManager("../../Data/dictionary.json");
            var sortedDictionary = dataManager.LoadData<IList<Word>>().OrderBy(word => word.Name);
            Dictionary = new ObservableCollection<Word>(sortedDictionary);
            DataContext = this;
        }

        private void ButtonAddWord_Click(object sender, RoutedEventArgs e) => new AddWordForm(Dictionary).Show();

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem listBoxItem) 
                new WordDetails(listBoxItem.Content as Word, Dictionary).Show();
        }
    }
}