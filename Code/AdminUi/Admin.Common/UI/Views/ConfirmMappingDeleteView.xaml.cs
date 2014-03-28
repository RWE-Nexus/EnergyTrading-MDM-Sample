namespace Common.UI.Views
{
    using System;

    using Common.UI.ViewModels;

    using Microsoft.Practices.Prism;

    public partial class ConfirmMappingDeleteView : IActiveAware
    {
        public ConfirmMappingDeleteView(ConfirmMappingDeleteViewModel viewModel)
        {
            this.DataContext = viewModel;
            this.InitializeComponent();
        }

        public event EventHandler IsActiveChanged = delegate { };

        public bool IsActive { get; set; }
    }
}