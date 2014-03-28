namespace Common.Events
{
    public class DialogOpenEvent
    {
        public DialogOpenEvent(bool dialogIsOpen)
        {
            this.DialogIsOpen = dialogIsOpen;
        }

        public bool DialogIsOpen { get; set; }
    }
}