namespace Common.Events
{
    public class CanSaveEvent
    {
        public CanSaveEvent(bool canSave)
        {
            this.CanSave = canSave;
        }

        public bool CanSave { get; set; }
    }
}