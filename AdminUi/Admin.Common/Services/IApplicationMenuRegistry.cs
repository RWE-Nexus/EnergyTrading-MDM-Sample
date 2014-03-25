namespace Common.Services
{
    using System;

    public interface IApplicationMenuRegistry
    {
        void RegisterEntitySelector(string location, Type type);

        void RegisterMenuItem(
            string menuItemName, 
            string description, 
            Type searchResultsViewType, 
            Uri addNewEntityUri, 
            string searchKey);

        void RegisterMenuItem(
            string menuItemName, 
            string description, 
            Type searchResultsViewType, 
            Uri addNewEntityUri, 
            string searchKey, 
            string searchLabel, 
            string baseEntityName = null);
    }
}