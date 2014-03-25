namespace Common.Events
{
    public class StatusEvent
    {
        public StatusEvent(string statusMessage)
        {
            this.StatusMessage = statusMessage;
        }

        public string StatusMessage { get; private set; }
    }
}