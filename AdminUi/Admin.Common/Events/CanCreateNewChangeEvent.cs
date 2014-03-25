namespace Common.Events
{
    public class CanCreateNewChangeEvent
    {
        public CanCreateNewChangeEvent(bool canCreate)
        {
            CanCreate = canCreate;
        }

        public bool CanCreate { get; private set; }
    }
}
