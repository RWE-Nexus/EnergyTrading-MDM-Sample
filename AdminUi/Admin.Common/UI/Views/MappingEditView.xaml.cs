namespace Common.UI.Views
{
    using System.Windows.Controls;
    using Common.UI.ViewModels;

    public partial class MappingEditView : UserControl
    {
        public MappingEditView(MappingEditViewModel mappingEditViewModel)
        {
            this.DataContext = mappingEditViewModel;
            this.InitializeComponent();
        }
    }
}