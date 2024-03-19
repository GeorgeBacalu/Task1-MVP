using Mvp1.Project.Data;
using Mvp1.Project.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Mvp1.Project.Modules.Administrative
{
    public partial class WordDetails : Window
    {
        public Word Word { get; set; }
        public ObservableCollection<Word> Dictionary { get; set; }
        public BitmapImage WordImage { get; set; }

        public WordDetails(Word word, ObservableCollection<Word> dictionary)
        {
            InitializeComponent();
            Word = word;
            Dictionary = dictionary;
            DataContext = this;
            LoadImage();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => new DictionaryManager().Show();

        private void UpdateButton_Click(object sender, RoutedEventArgs e) => new UpdateWordForm(Word, Dictionary).Show();

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this word?", "Delete Word", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Dictionary.Remove(Word);
                new DataManager("../../Data/dictionary.json").SaveData(Dictionary);
                Close();
            }
        }

        private void LoadImage()
        {
            if (!string.IsNullOrWhiteSpace(Word.Image))
            {
                string fullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Word.Image));
                WordImage = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
            }
        }
    }
}