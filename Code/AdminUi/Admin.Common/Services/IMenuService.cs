namespace Common.Services
{
    using Microsoft.Practices.Prism.Commands;

    public interface IMenuService
    {
        CompositeCommand SaveCommand { get; set; }
    }
}