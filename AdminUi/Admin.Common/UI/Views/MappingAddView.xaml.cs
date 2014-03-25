namespace Common.UI.Views
{
    using System.Windows.Controls;
    using Common.UI.ViewModels;

    /// <summary>
    /// Interaction logic for MappingAddView.xaml
    /// </summary>
    public partial class MappingAddView : UserControl
    {
        public MappingAddView(MappingAddViewModel mappingAddViewModel)
        {
            this.DataContext = mappingAddViewModel;
            this.InitializeComponent();
        }
    }
}