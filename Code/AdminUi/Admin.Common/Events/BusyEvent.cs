namespace Common.Events
{
    public class BusyEvent
    {
        public BusyEvent(bool isBusy)
        {
            this.IsBusy = isBusy;
        }

        public bool IsBusy { get; private set; }
    }
}