﻿// This code was generated by a tool: ViewTemplates\EntityAddViewCodeTemplate.tt
namespace Admin.LocationModule.Views
{
    using System.Windows.Controls;

    using Admin.LocationModule.ViewModels;

    /// <summary>
    /// Interaction logic for LocationAddView.xaml
    /// </summary>
    public partial class LocationAddView : UserControl
    {
        public LocationAddView(LocationAddViewModel locationAddViewModel)
        {
            this.DataContext = locationAddViewModel;
            this.InitializeComponent();
        }
    }
}