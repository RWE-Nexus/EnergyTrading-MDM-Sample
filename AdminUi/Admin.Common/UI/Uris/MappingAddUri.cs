namespace Common.UI.Uris
{
    using System;

    public class MappingAddUri : Uri
    {
        public MappingAddUri(int partyId, string entityName) : 
            base(ViewNames.MappingAddView + string.Format("?{0}={1}&{2}={3}", NavigationParameters.EntityId, partyId, NavigationParameters.EntityName, entityName), UriKind.Relative)
        {
        }
        
        public MappingAddUri(int partyId, string entityName, string entityInstanceName) : 
            base(ViewNames.MappingAddView + string.Format("?{0}={1}&{2}={3}&{4}={5}", NavigationParameters.EntityId, partyId, NavigationParameters.EntityName, entityName, NavigationParameters.EntityInstanceName, entityInstanceName), UriKind.Relative)
        {
        }
    }
}