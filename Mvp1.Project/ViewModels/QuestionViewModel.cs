using Mvp1.Project.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace Mvp1.Project.ViewModels
{
    public class QuestionViewModel : INotifyPropertyChanged
    {
        private Word word;
        public Word Word { get => word; set => word = value; }

        private string definition;
        public string Definition { get => definition; set => definition = value; }

        private BitmapImage image;
        public BitmapImage Image { get => image; set => image = value; }

        private string guessedWord;
        public string GuessedWord { get => guessedWord; set => guessedWord = value; }

        public QuestionViewModel(Word word)
        {
            Word = word;
            Random random = new Random();
            var definitionShown = Word.Image == "../../Resources/Images/default.jpg" || random.Next(0, 2) == 0;
            Definition = definitionShown ? Word.Definition : null;
            Image = definitionShown ? null : new BitmapImage(new Uri(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Word.Image)), UriKind.Absolute));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}