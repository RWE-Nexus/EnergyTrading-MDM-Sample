namespace Common.Commands
{
    using Microsoft.Practices.Prism.Commands;

    public static class MenuCommands
    {
        private static CompositeCommand saveChangesCommand = new CompositeCommand(true);

        public static CompositeCommand SaveChangesCommand
        {
            get
            {
                return saveChangesCommand;
            }

            set
            {
                saveChangesCommand = value;
            }
        }
    }
}