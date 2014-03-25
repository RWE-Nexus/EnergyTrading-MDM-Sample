namespace Common.Events
{
    public class CanCloneChangeEvent
    {
        public CanCloneChangeEvent(bool canCreate)
        {
            CanClone = canCreate;
        }

        public bool CanClone { get; private set; }
    }
}