namespace Common.UI.Uris
{
    using System;

    public class EntityEditUri : Uri
    {
        public EntityEditUri(string entityName, int entityId) : 
            base(entityName + "EditView" + string.Format("?{0}={1}", NavigationParameters.EntityId, entityId), UriKind.Relative)
        {
        }

        public EntityEditUri(string entityName, int? entityId, DateTime validAt) : 
            base(entityName + "EditView" + string.Format("?{0}={1}&{2}={3}", NavigationParameters.EntityId, entityId, NavigationParameters.ValidAtDate, validAt), UriKind.Relative)
        {
        }
    }
}