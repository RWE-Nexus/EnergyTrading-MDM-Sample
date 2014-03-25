namespace Shell.Views
{
    using System.Windows.Controls;

    using Shell.ViewModels;

    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView(SearchViewModel searchViewModel)
        {
            this.DataContext = searchViewModel;
            this.InitializeComponent();
        }
    }
}