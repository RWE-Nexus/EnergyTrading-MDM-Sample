using Microsoft.Practices.Prism.ViewModel;

namespace Common.UI.ViewModels
{
    public class SelectorViewModel : NotificationObject
    {
        public SelectorViewModel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
    }
}