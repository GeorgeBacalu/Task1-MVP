﻿using Microsoft.Win32;
using Mvp1.Project.Data;
using Mvp1.Project.Models;
using Mvp1.Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Mvp1.Project.Modules.Administrative
{
    public partial class AddWordForm : Window
    {
        private readonly DataManager dictionaryDataManager = new DataManager("../../Data/dictionary.json");
        private readonly DataManager categoryDataManager = new DataManager("../../Data/categories.json");
        public ObservableCollection<Word> Dictionary { get; set; }
        public IList<Category> Categories { get; set; }

        public AddWordForm(ObservableCollection<Word> dictionary)
        {
            InitializeComponent();
            Dictionary = dictionary;
            Categories = categoryDataManager.LoadData<IList<Category>>();
            DataContext = new WordFormViewModel { Categories = Categories };
        }

        private void ButtonAddWordSubmit_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as WordFormViewModel;
            string name = TextName.Text;
            string definition = TextDefinition.Text;
            string imagePath = viewModel.ImagePath ?? "../../Resources/Images/default.jpg";
            string categoryName = (ComboBoxCategory.SelectedItem as Category)?.Name ?? ComboBoxCategory.Text;

            if (Dictionary.Any(word => word.Name == name)) { MessageBox.Show($"Word '{name}' already exists!"); return; }
            Category category = Categories.FirstOrDefault(c => c.Name == categoryName) ?? new Category { Id = Categories.Count + 1, Name = categoryName };
            if (!Categories.Contains(category))
            {
                Categories.Add(category);
                categoryDataManager.SaveData(Categories);
            }
            Dictionary.Add(new Word { Id = Dictionary.Count + 1, Name = name, Definition = definition, Image = imagePath, Category = category });
            var sortedDictionary = new ObservableCollection<Word>(Dictionary.OrderBy(word => word.Name));
            Dictionary.Clear();
            foreach (Word word in sortedDictionary) Dictionary.Add(word);
            dictionaryDataManager.SaveData(Dictionary);
            Close();
        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                string imageFileName = Path.GetFileName(fileName);
                string imageDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "Resources", "Images");
                Directory.CreateDirectory(imageDirectory);
                string destinationPath = Path.Combine(imageDirectory, imageFileName);
                if (!File.Exists(destinationPath)) File.Copy(fileName, destinationPath);
                var viewModel = DataContext as WordFormViewModel;
                viewModel.ImagePath = $"../../Resources/Images/{imageFileName}";
                viewModel.Image = new BitmapImage(new Uri(Path.GetFullPath(destinationPath), UriKind.Absolute));
                viewModel.IsImageUploaded = true;
            }
        }
    }
}