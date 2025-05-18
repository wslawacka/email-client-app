using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using MsBox.Avalonia;

namespace EmailClient;

public partial class MainWindow : Window
{
    private ViewModel _viewModel;
    
    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new ViewModel();
        this.DataContext = _viewModel;
        
        FindMail.OnSearchQueryChanged += HandleQuerySearch;
        FindMail.OnCategoryChanged += HandleCategorySearch;
        FindMail.OnReset += HandleReset;
        
        FindMail.Categories = new ObservableCollection<string>(new List<string> { "subject", "sender", "recipient" });
        FindMail.SelectedCategory = "subject";
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void HandleQuerySearch(object sender, string query)
    {
        _viewModel.FilterEmails(query, _viewModel.CurrentCategory);
    }
    
    private void HandleCategorySearch(object sender, string category)
    {
        _viewModel.CurrentCategory = category;
        _viewModel.FilterEmails(_viewModel.CurrentQuery, category);
    }

    private void HandleReset(object sender, EventArgs e)
    {
        _viewModel.ResetSearch();
    }
        
}