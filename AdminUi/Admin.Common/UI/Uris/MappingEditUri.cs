namespace Common.UI.Uris
{
    using System;

    public class MappingEditUri : Uri
    {
        public MappingEditUri(int entityId, string entityName, int mappingId) : 
            base(ViewNames.MappingEditView + string.Format("?{0}={1}&{2}={3}&{4}={5}", NavigationParameters.EntityId, entityId, NavigationParameters.EntityName, entityName, NavigationParameters.MappingId, mappingId), UriKind.Relative)
        {
        } 

        public MappingEditUri(int entityId, string entityName, int mappingId, string entityInstanceName) : 
            base(ViewNames.MappingEditView + string.Format("?{0}={1}&{2}={3}&{4}={5}&{6}={7}", NavigationParameters.EntityId, entityId, NavigationParameters.EntityName, entityName, NavigationParameters.MappingId, mappingId, NavigationParameters.EntityInstanceName, entityInstanceName), UriKind.Relative)
        {
        } 
    }
}