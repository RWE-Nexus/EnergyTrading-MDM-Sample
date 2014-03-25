namespace Common.UI.Views
{
    using System;

    using Common.UI.ViewModels;

    using Microsoft.Practices.Prism;

    public partial class MappingUpdateView : IActiveAware
    {
        public MappingUpdateView(MappingUpdateViewModel mappingUpdateViewModel)
        {
            this.DataContext = mappingUpdateViewModel;
            this.InitializeComponent();
        }

        public event EventHandler IsActiveChanged = delegate { };

        public bool IsActive { get; set; }
    }
}