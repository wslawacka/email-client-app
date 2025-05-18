using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Runtime.CompilerServices;

namespace EmailClient
{
    public partial class FindMail : UserControl, INotifyPropertyChanged
    {
        public FindMail()
        {
            InitializeComponent();
            DataContext = this;
        }

        public new event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string? _query;
        public string? Query
        {
            get => _query;
            set
            {
                if (_query != value)
                {
                    _query = value;
                    OnPropertyChanged();
                    OnSearchQueryChanged?.Invoke(this, _query ?? "");
                }
            }
        }

        private string? _selectedCategory;
        public string? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                    OnCategoryChanged?.Invoke(this, _selectedCategory ?? "");
                }
            }
        }

        private IEnumerable<string>? _categories;
        public IEnumerable<string>? Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        // Callback events
        public delegate void CategoryChangedCallback(object sender, string category);
        public event CategoryChangedCallback? OnCategoryChanged;

        public delegate void SearchQueryChangedCallback(object sender, string query);
        public event SearchQueryChangedCallback? OnSearchQueryChanged;

        public delegate void ResetCallback(object sender, RoutedEventArgs e);
        public event ResetCallback? OnReset;

        private void ResetButton_OnClick(object? sender, RoutedEventArgs e)
        {
            Query = string.Empty;
            SelectedCategory = "subject";
            OnReset?.Invoke(this, e);
        }
    }
}
