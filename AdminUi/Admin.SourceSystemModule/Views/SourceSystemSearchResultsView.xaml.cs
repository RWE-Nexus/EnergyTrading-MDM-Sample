﻿// This code was generated by a tool: ViewTemplates\EntitySearchResultsViewCodeTemplate.tt
namespace Admin.SourceSystemModule.Views
{
    using System.Windows.Controls;

    using Admin.SourceSystemModule.ViewModels;

    /// <summary>
    /// Interaction logic for SourceSystemSearchResultsView.xaml
    /// </summary>
    public partial class SourceSystemSearchResultsView : UserControl
    {
        public SourceSystemSearchResultsView(SourceSystemSearchResultsViewModel sourcesystemSearchResultsViewModel)
        {
            this.DataContext = sourcesystemSearchResultsViewModel;
            this.InitializeComponent();
        }
    }
}