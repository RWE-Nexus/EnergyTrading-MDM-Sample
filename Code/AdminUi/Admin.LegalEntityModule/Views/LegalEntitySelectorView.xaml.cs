﻿// This code was generated by a tool: ViewTemplates\EntitySelectorViewCodeTemplate.tt
namespace Admin.LegalEntityModule.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using Admin.LegalEntityModule.ViewModels;

    /// <summary>
    /// Interaction logic for LegalEntitySelectorView.xaml
    /// </summary>
    public partial class LegalEntitySelectorView : UserControl
    {
        public LegalEntitySelectorView(LegalEntitySelectorViewModel legalentitySelectorViewModel)
        {
            this.DataContext = legalentitySelectorViewModel;
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        public void SelectLegalEntityMDC(object sender, MouseButtonEventArgs e)
        {
            ((LegalEntitySelectorViewModel)DataContext).SelectLegalEntity();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(SearchCriteriaTextBox);
        }
    }
}