namespace Admin.ReferenceDataModule.Views
{
    using System.Windows.Controls;
    using Admin.ReferenceDataModule.ViewModels;

    /// <summary>
    /// Interaction logic for ReferenceDataEditView.xaml
    /// </summary>
    public partial class ReferenceDataEditView : UserControl
    {
        public ReferenceDataEditView(ReferenceDataEditViewModel referenceDataEditViewModel)
        {
            this.DataContext = referenceDataEditViewModel;
            this.InitializeComponent();
        }
    }
}
