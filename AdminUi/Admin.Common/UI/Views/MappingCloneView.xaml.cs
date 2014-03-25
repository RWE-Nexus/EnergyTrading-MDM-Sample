namespace Common.UI.Views
{
    using System.Windows.Controls;
    using Common.UI.ViewModels;

    public partial class MappingCloneView : UserControl
    {
        public MappingCloneView(MappingCloneViewModel mappingEditViewModel)
        {
            this.DataContext = mappingEditViewModel;
            this.InitializeComponent();
        }
    }
}