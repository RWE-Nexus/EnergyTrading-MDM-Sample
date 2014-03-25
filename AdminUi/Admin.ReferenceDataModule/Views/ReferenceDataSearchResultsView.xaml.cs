namespace Admin.ReferenceDataModule.Views
{
    using System.Windows.Controls;

    using Admin.ReferenceDataModule.ViewModels;

    /// <summary>
    /// Interaction logic for ReferenceDataSearchResultsView.xaml
    /// </summary>
    public partial class ReferenceDataSearchResultsView : UserControl
    {
        public ReferenceDataSearchResultsView(ReferenceDataSearchResultsViewModel referenceDataSearchResultsViewModel)
        {
            this.DataContext = referenceDataSearchResultsViewModel;
            this.InitializeComponent();
        }
    }
}