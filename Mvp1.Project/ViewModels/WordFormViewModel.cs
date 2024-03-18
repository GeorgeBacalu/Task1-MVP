using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mvp1.Project.Models;
using System.Linq;
using Mvp1.Project.Commands;
using System.Windows;

namespace Mvp1.Project.ViewModels
{
    public class WordFormViewModel : INotifyDataErrorInfo
    {
        private string name;
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get => name; set { name = value; Validate(value, nameof(Name)); } }

        private string definition;
        [Required(ErrorMessage = "Definition is required!")]
        public string Definition { get => definition; set { definition = value; Validate(value, nameof(Definition)); } }

        private string imageUrl;
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }

        private ECategory category;
        public ECategory Category { get => category; set => category = value; }
        public IList<ECategory> Categories => Enum.GetValues(typeof(ECategory)).Cast<ECategory>().ToList();

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

        private void Submit(object obj) => MessageBox.Show("Word added successfully!");

        private bool CanSubmit(object obj) => Validator.TryValidateObject(this, new ValidationContext(this), null);
    }
}