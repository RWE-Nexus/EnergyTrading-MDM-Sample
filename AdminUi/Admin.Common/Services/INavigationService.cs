namespace Common.Services
{
    using System;

    using Common.Events;

    public interface INavigationService
    {
        event EventHandler NavigationCleared;

        void ClearHistory();

        void NavigateMain(Uri navigateUri);

        void NavigateMainBack();

        void NavigateMainBackWithStatus(StatusEvent statusEvent);

        void NavigateMainForward();

        void NavigateToSearch();
    }
}