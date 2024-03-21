using Mvp1.Project.Commands;
using Mvp1.Project.Data;
using Mvp1.Project.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        public QuizViewModel()
        {
            Dictionary = dictionaryDataManager.LoadData<ObservableCollection<Word>>();
            Random random = new Random();
            var randomQuestions = Dictionary.OrderBy(x => random.Next()).Take(5).ToList();
            Questions = new ObservableCollection<Word>(randomQuestions);
            CurrentQuestion = 1;
            CurrentQuestionView = new QuestionViewModel(Questions[CurrentQuestion - 1]);
            NavigationCommand = new NavigationCommand(Navigate, CanNavigate);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string NavigationButtonText => CurrentQuestion == questions.Count ? "Finish" : "Next";

        private void Navigate(object obj)
        {
            if (obj.ToString() == "Previous" && CurrentQuestion > 1) --CurrentQuestion;
            else if (obj.ToString() == "Next" && CurrentQuestion < Questions.Count) ++CurrentQuestion;
            CurrentQuestionView = new QuestionViewModel(Questions[CurrentQuestion - 1]);
            OnPropertyChanged(nameof(CurrentQuestionView));
            OnPropertyChanged(nameof(NavigationButtonText));
            MessageBox.Show()
        }

        private bool CanNavigate(object obj) => CurrentQuestion <= Questions.Count;
    }
}