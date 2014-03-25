namespace Common.Services
{
    using System;
    using System.Collections.Generic;

    public interface IApplicationCommands
    {
        void CloseView(Type viewType);

        void CloseView(Type viewType, string regionName);

        void CloseView(string viewName, string regionName);

        void OpenView(Type viewType, string activeEntity, string regionName);

        void OpenView(Type viewType, string activeEntity, string selectedPropertyName, string regionName);

        void OpenView(string viewType, string regionName, int producttypeId, DateTime? validAtString);

        void OpenView(string viewType, string regionName, int producttypeId, DateTime? validAtString, string contextType);

        void OpenView(Type viewType, string regionName, IDictionary<String, String> parameters);
    }
}