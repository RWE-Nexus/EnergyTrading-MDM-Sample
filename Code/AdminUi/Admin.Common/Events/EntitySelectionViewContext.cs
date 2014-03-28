namespace Common.Events
{
    public class EntitySelectionViewContext
    {
        public EntitySelectionViewContext(string activeEntity, string selectedPropertyName)
        {
            this.ActiveEntity = activeEntity;
            this.SelectedPropertyName = selectedPropertyName;
        }

        public string ActiveEntity { get; set; }

        public string SelectedPropertyName { get; set; }
    }
}