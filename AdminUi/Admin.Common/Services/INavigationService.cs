namespace Common.Services
{
    using System;
    using Common.Events;

    public interface INavigationService
    {
        void NavigateMain(Uri navigateUri);

        void NavigateMainBack();

        void NavigateMainForward();

        void NavigateToSearch();

        void NavigateMainBackWithStatus(StatusEvent statusEvent);

        void ClearHistory();

        event EventHandler NavigationCleared;
    }
}