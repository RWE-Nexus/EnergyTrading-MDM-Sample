namespace Admin.ReferenceDataModule.Views
{
    using System.Windows.Controls;
    using Admin.ReferenceDataModule.ViewModels;

    /// <summary>
    /// Interaction logic for ReferenceDataAddView.xaml
    /// </summary>
    public partial class ReferenceDataAddView : UserControl
    {
        public ReferenceDataAddView(ReferenceDataAddViewModel referenceDataAddViewModel)
        {
            this.DataContext = referenceDataAddViewModel;
            this.InitializeComponent();
        }
    }
}
