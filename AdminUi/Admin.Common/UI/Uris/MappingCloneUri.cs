namespace Common.UI.Uris
{
    using System;

    public class MappingCloneUri : Uri
    {
        public MappingCloneUri(int entityId, string entityName, int originalEntityId, int mappingId, string entityInstanceName) :
            base(
                ViewNames.MappingCloneView + string.Format("?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}", 
                    NavigationParameters.EntityId, 
                    entityId, 
                    NavigationParameters.EntityName, 
                    entityName,
                    NavigationParameters.OriginalEntityId,
                    originalEntityId,
                    NavigationParameters.MappingId, 
                    mappingId, 
                    NavigationParameters.EntityInstanceName, 
                    entityInstanceName), 
                UriKind.Relative)
        {
        } 
    }
}