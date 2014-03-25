namespace Common.Services
{
    using Common.Commands;
    using Microsoft.Practices.Prism.Commands;

    public class MenuService : IMenuService
    {
        public CompositeCommand SaveCommand
        {
            get
            {
                return MenuCommands.SaveChangesCommand;
            }

            set
            {
                MenuCommands.SaveChangesCommand = value;
            }
        }
    }
}