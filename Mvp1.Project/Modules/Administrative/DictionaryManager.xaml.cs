using Mvp1.Project.Data;
using Mvp1.Project.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Mvp1.Project.Modules.Administrative
{
    public partial class DictionaryManager : Window
    {
        public ObservableCollection<Word> Dictionary { get; private set; }

        public DictionaryManager()
        {
            InitializeComponent();
            DataManager dataManager = new DataManager("../../Data/dictionary.json");
            IList<Word> sortedDictionary = dataManager.LoadData<IList<Word>>().OrderBy(word => word.Name).ToList();
            Dictionary = new ObservableCollection<Word>(sortedDictionary);
            DataContext = this;
        }
    }
}