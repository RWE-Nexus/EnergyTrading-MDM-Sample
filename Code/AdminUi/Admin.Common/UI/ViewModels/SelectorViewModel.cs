namespace Common.UI.ViewModels
{
    using Microsoft.Practices.Prism.ViewModel;

    public class SelectorViewModel : NotificationObject
    {
        public SelectorViewModel(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}