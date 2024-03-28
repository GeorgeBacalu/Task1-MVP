using Mvp1.Project.Commands;
using Mvp1.Project.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Mvp1.Project.ViewModels
{
    public class QuestionViewModel : INotifyPropertyChanged
    {
        private Word word;
        public Word Word { get => word; set { word = value; OnPropertyChanged(nameof(Word)); } }

        private string definition;
        public string Definition { get => definition; set { definition = value; OnPropertyChanged(nameof(Definition)); } }

        private BitmapImage image;
        public BitmapImage Image { get => image; set { image = value; OnPropertyChanged(nameof(Image)); } }

        private string guessedWord;
        public string GuessedWord
        {
            get => guessedWord;
            set
            {
                if (guessedWord != value)
                {
                    guessedWord = value;
                    OnPropertyChanged(nameof(GuessedWord));
                    (checkWordCommand as CheckWordCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private ICommand checkWordCommand;
        public ICommand CheckWordCommand { get => checkWordCommand; set => checkWordCommand = value; }

        public QuestionViewModel(Word word, ICommand checkWordCommand)
        {
            this.checkWordCommand = checkWordCommand;
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