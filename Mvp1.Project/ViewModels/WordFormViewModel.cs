using Mvp1.Project.Models;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;

namespace Mvp1.Project.ViewModels
{
    public class WordFormViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string name;
        public string Name { get => name; set { name = value; NotifyPropertyChanged(nameof(Name)); SyncValidity(); } }

        private string definition;
        public string Definition { get => definition; set { definition = value; NotifyPropertyChanged(nameof(Definition)); SyncValidity(); } }

        private string imageUrl;
        public string ImageUrl { get => imageUrl; set { imageUrl = value; NotifyPropertyChanged(nameof(ImageUrl)); } }

        private ECategory category;
        public ECategory Category { get => category; set { category = value; NotifyPropertyChanged(nameof(Category)); } }
        public IList<ECategory> Categories { get => Enum.GetValues(typeof(ECategory)).Cast<ECategory>().ToList(); }

        private bool isValid;
        public bool IsValid { get => isValid; set { isValid = value; NotifyPropertyChanged(nameof(IsValid)); } }
        public void SyncValidity() => IsValid = !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Definition);

        public string Error => null;
        public string this[string propertyName] =>
            propertyName == nameof(Name) && string.IsNullOrWhiteSpace(Name) ? "Name is required!" :
            propertyName == nameof(Definition) && string.IsNullOrWhiteSpace(Definition) ? "Definition is required!" : "";
    }
}