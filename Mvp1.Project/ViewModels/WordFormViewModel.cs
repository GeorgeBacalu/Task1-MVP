using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mvp1.Project.Models;
using System.Linq;
using Mvp1.Project.Commands;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

namespace Mvp1.Project.ViewModels
{
    public class WordFormViewModel : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string name;
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get => name; set { name = value; Validate(value, nameof(Name)); } }

        private string definition;
        [Required(ErrorMessage = "Definition is required!")]
        public string Definition { get => definition; set { definition = value; Validate(value, nameof(Definition)); } }

        private string imagePath;
        public string ImagePath { get => imagePath; set { imagePath = value; UpdateImageSource(imagePath); } }

        private void UpdateImageSource(string relativePath)
        {
            string fullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath));
            Image = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
            IsImageUploaded = true;
        }

        private BitmapImage image;
        public BitmapImage Image { get => image; set { image = value; OnPropertyChanged(nameof(Image)); } }

        private bool isImageUploaded;
        public bool IsImageUploaded { get => isImageUploaded; set { isImageUploaded = value; OnPropertyChanged(nameof(IsImageUploaded)); } }

        private string categoryName;
        [Required(ErrorMessage = "Category is required!")]
        public string CategoryName { get => categoryName; set { categoryName = value; Validate(value, nameof(CategoryName)); } }

        private IList<Category> categories;
        public IList<Category> Categories { get => categories; set => categories = value; }

        Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        public bool HasErrors => Errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName) => Errors.ContainsKey(propertyName) ? Errors[propertyName] : Enumerable.Empty<string>();
    
        public void Validate(object propertyValue, string propertyName)
        {
            var results = new List<ValidationResult>();
            if (Errors.ContainsKey(propertyName)) Errors.Remove(propertyName);
            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);
            if (results.Any()) Errors.Add(propertyName, results.Select(result => result.ErrorMessage).ToList());
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            SubmitCommand.RaiseCanExecuteChanged();
        }

        public ActionCommand SubmitCommand { get; set; }

        public WordFormViewModel() => SubmitCommand = new ActionCommand(Submit, CanSubmit);

        private void Submit(object obj) => MessageBox.Show("Word saved successfully!");

        private bool CanSubmit(object obj) => Validator.TryValidateObject(this, new ValidationContext(this), null);
    }
}