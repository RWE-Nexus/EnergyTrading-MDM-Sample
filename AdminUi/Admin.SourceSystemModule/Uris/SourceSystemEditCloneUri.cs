﻿// This code was generated by a tool: InfrastructureTemplates\EntityEditCloneUriTemplate.tt
namespace Admin.SourceSystemModule.Uris
{
    using System;

    using Admin.SourceSystemModule.Views;

    using Common;

    public class SourceSystemEditCloneUri : Uri
    {
        public SourceSystemEditCloneUri(
            int sourcesystemId, 
            DateTime validAt, 
            int originalsourcesystemId, 
            DateTime originalValidAt)
            : base(
                SourceSystemViewNames.SourceSystemEditCloneView
                + string.Format(
                    "?{0}={1}&{2}={3}&{4}={5}&{6}={7}", 
                    NavigationParameters.EntityId, 
                    sourcesystemId, 
                    NavigationParameters.ValidAtDate, 
                    validAt, 
                    NavigationParameters.OriginalEntityId, 
                    originalsourcesystemId, 
                    NavigationParameters.OriginalValidAtDate, 
                    originalValidAt), 
                UriKind.Relative)
        {
        }
    }
}