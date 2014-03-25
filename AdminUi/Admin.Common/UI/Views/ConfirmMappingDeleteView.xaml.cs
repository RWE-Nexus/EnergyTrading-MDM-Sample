using System;
using Microsoft.Practices.Prism;

namespace Common.UI.Views
{
    using Common.UI.ViewModels;

    public partial class ConfirmMappingDeleteView : IActiveAware
    {
        private bool isActive;

        public ConfirmMappingDeleteView(ConfirmMappingDeleteViewModel viewModel)
        {
            this.DataContext = viewModel;
            this.InitializeComponent();
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public event EventHandler IsActiveChanged = delegate { };
    }
}
