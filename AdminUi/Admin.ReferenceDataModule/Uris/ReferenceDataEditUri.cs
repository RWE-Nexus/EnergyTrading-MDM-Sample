namespace Admin.ReferenceDataModule.Uris
{
    using System;
    using Admin.ReferenceDataModule.Views;
    using Common;

    public class ReferenceDataEditUri : Uri
    {
        public ReferenceDataEditUri(string referenceKey, string value) : 
            base(ReferenceDataViewNames.ReferenceDataEditView + string.Format("?{0}={1}&{2}={3}", NavigationParameters.EntityId, referenceKey, NavigationParameters.EntityValue, value), UriKind.Relative)
        {
        }
    }
}
	