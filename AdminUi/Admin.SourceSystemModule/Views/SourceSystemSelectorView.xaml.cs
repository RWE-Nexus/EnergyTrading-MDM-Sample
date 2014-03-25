﻿// This code was generated by a tool: ViewTemplates\EntitySelectorViewCodeTemplate.tt

using System.Windows;
using System.Windows.Input;

namespace Admin.SourceSystemModule.Views
{
    using System.Windows.Controls;
    using Admin.SourceSystemModule.ViewModels;

    /// <summary>
    /// Interaction logic for SourceSystemSelectorView.xaml
    /// </summary>
    public partial class SourceSystemSelectorView : UserControl
    {
        public SourceSystemSelectorView(SourceSystemSelectorViewModel sourcesystemSelectorViewModel)
        {
            this.DataContext = sourcesystemSelectorViewModel;
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        public void SelectSourceSystemMDC(object sender, MouseButtonEventArgs e)
        {
            ((SourceSystemSelectorViewModel)DataContext).SelectSourceSystem();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(SearchCriteriaTextBox);
        }
    }
}