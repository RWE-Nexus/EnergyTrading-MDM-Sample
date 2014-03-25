namespace Common.Events
{
    public class CanSaveEvent
    {
        public bool CanSave { get; set; }

        public CanSaveEvent(bool canSave)
        {
            this.CanSave = canSave;
        }
    }
}