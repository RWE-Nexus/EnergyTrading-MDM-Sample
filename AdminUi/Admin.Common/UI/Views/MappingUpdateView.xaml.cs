using System;
using Microsoft.Practices.Prism;

namespace Common.UI.Views
{
    using Common.UI.ViewModels;

    public partial class MappingUpdateView : IActiveAware
    {
        private bool isActive;

        public MappingUpdateView(MappingUpdateViewModel mappingUpdateViewModel)
        {
            this.DataContext = mappingUpdateViewModel;
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
