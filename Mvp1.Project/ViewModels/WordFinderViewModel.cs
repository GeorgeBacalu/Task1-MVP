using Mvp1.Project.Commands;
using Mvp1.Project.Data;
using Mvp1.Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Mvp1.Project.ViewModels
{
    public class WordFinderViewModel : INotifyPropertyChanged
    {
        private readonly DataManager dictionaryDataManager = new DataManager("../../Data/dictionary.json");
        private readonly DataManager categoryDataManager = new DataManager("../../Data/categories.json");

        private ObservableCollection<Word> words;
        public ObservableCollection<Word> Words { get => words; set { words = value; OnPropertyChanged(nameof(Words)); } }

        private IList<Category> categories;
        public IList<Category> Categories { get => categories; set { categories = value; OnPropertyChanged(nameof(Categories)); } }

        private Category selectedCategory;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                    ApplyFilters();
                }
            }
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    ApplyFilters();
                }
            }
        }

        private ObservableCollection<Word> filteredWords;
        public ObservableCollection<Word> FilteredWords { get => filteredWords; set { filteredWords = value; OnPropertyChanged(nameof(FilteredWords)); } }

        private Word selectedWord;
        public Word SelectedWord { get => selectedWord; set { selectedWord = value; OnPropertyChanged(nameof(SelectedWord)); } }

        private BitmapImage wordImage;
        public BitmapImage WordImage { get => wordImage; set { wordImage = value; OnPropertyChanged(nameof(WordImage)); } }

        public ICommand SearchCommand { get; set; }

        private void ApplyFilters()
        {
            var results = Words.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(SelectedCategory.Name))
                results = results.Where(word => word.Category.Name.Equals(SelectedCategory.Name, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(SearchText))
                results = results.Where(word => word.Name.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase));
            FilteredWords = new ObservableCollection<Word>(results);
        }

        public WordFinderViewModel()
        {
            Words = dictionaryDataManager.LoadData<ObservableCollection<Word>>();
            Categories = categoryDataManager.LoadData<IList<Category>>();
            FilteredWords = Words;
            SearchCommand = new SearchCommand(PerformSearch, CanPerformSearch);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void PerformSearch(object obj)
        {
            SelectedWord = FilteredWords.FirstOrDefault(word => word.Name.Equals(SearchText, StringComparison.OrdinalIgnoreCase));
            LoadImage(SelectedWord);
        }

        private bool CanPerformSearch(object obj) => !string.IsNullOrWhiteSpace(SearchText) && FilteredWords != null && FilteredWords.Any();

        private void LoadImage(Word word)
        {
            if (!string.IsNullOrWhiteSpace(word.Image))
            {
                string fullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, word.Image));
                WordImage = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
            }
        }
    }
}