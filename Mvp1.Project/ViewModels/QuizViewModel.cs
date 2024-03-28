using Mvp1.Project.Commands;
using Mvp1.Project.Data;
using Mvp1.Project.Models;
using Mvp1.Project.Modules.Entertainment;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Input;

namespace Mvp1.Project.ViewModels
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        private readonly DataManager dictionaryDataManager = new DataManager("../../Data/dictionary.json");

        private ObservableCollection<Word> dictionary;
        public ObservableCollection<Word> Dictionary { get => dictionary; set { dictionary = value; OnPropertyChanged(nameof(Dictionary)); } }

        private ObservableCollection<Word> questions;
        public ObservableCollection<Word> Questions { get => questions; set { questions = value; OnPropertyChanged(nameof(Questions)); } }

        private int currentQuestion;
        public int CurrentQuestion { get => currentQuestion; set { currentQuestion = value; OnPropertyChanged(nameof(CurrentQuestion)); } }

        private QuestionViewModel currentQuestionView;
        public QuestionViewModel CurrentQuestionView { get => currentQuestionView; set { currentQuestionView = value; OnPropertyChanged(nameof(CurrentQuestionView)); } }

        private ICommand navigationCommand;
        public ICommand NavigationCommand { get => navigationCommand; set { navigationCommand = value; OnPropertyChanged(nameof(NavigationCommand)); } }

        private ICommand checkWordCommand;
        public ICommand CheckWordCommand { get => checkWordCommand; set { checkWordCommand = value; OnPropertyChanged(nameof(CheckWordCommand)); } }

        public QuizViewModel()
        {
            Dictionary = dictionaryDataManager.LoadData<ObservableCollection<Word>>();
            Random random = new Random();
            var randomQuestions = Dictionary.OrderBy(x => random.Next()).Take(5).ToList();
            Questions = new ObservableCollection<Word>(randomQuestions);
            CurrentQuestion = 1;
            CheckWordCommand = new CheckWordCommand(CheckWord, CanCheckWord);
            CurrentQuestionView = new QuestionViewModel(Questions[CurrentQuestion - 1], CheckWordCommand);
            NavigationCommand = new NavigationCommand(Navigate, CanNavigate);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string NavigationButtonText => CurrentQuestion == Questions.Count ? "Finish" : "Next";

        public string PreviousButtonVisibility => CurrentQuestion == 1 ? "Hidden" : "Visible";

        private void Navigate(object direction)
        {
            if ((direction as string) == "Previous" && CurrentQuestion > 1) --CurrentQuestion;
            else if ((direction as string) == "Next" && CurrentQuestion < Questions.Count) ++CurrentQuestion;
            else if ((direction as string) == "Next" && CurrentQuestion == Questions.Count) new ResultsScreen().Show();
            CurrentQuestionView = new QuestionViewModel(Questions[CurrentQuestion - 1], CheckWordCommand);
            CurrentQuestionView.GuessedWord = "";
            OnPropertyChanged(nameof(CurrentQuestionView));
            OnPropertyChanged(nameof(NavigationButtonText));
            OnPropertyChanged(nameof(PreviousButtonVisibility));
            CurrentQuestionView.OnPropertyChanged(nameof(QuestionViewModel.GuessedWord));
        }

        private bool CanNavigate(object obj) => CurrentQuestion <= Questions.Count;

        private void CheckWord(object obj)
        {
            Word correctWord = CurrentQuestionView.Word;
            if (CurrentQuestionView.GuessedWord != null && CurrentQuestionView.GuessedWord.Equals(correctWord.Name, StringComparison.OrdinalIgnoreCase))
                MessageBox.Show("Correct!", "Quiz", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                MessageBox.Show($"Wrong! Correct answer was: {correctWord.Name}", "Quiz", MessageBoxButton.OK, MessageBoxImage.Information);
            }   
        }

        private bool CanCheckWord(object obj) => true;
    }
}