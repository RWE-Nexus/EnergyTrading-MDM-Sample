namespace Common
{
    using System;
    using System.Collections.Generic;

    public class EntityMetaData
    {
        public EntityMetaData()
        {
            this.PropertiesWithReferenceData = new Dictionary<string, string>();
            this.PropertiesWithDifferentEntityName = new Dictionary<string, string>();
        }

        /// <summary>
        /// Entity name, e.g.: Product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Entity details type, e.g.: typeof(ProductDetails)
        /// </summary>
        public Type DetailsType { get; set; }

        /// <summary>
        /// Entity type, e.g.: typeof(Product)
        /// </summary>
        public Type ParentType { get; set; }

        /// <summary>
        /// Admin project module relative path, e.g.:@"..\Admin.ProductModule\"
        /// </summary>
        public string ProjectFolder { get; set; }

        /// <summary>
        /// Entity module project relative file path, e.g.: @"..\Admin.ProductModule\Admin.ProductModule.csproj"
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Entity key, e.g.: Name
        /// </summary>
        public string EntityKey { get; set; }

        /// <summary>
        /// Entity data contract key, e.g: Name. This can be different than the actual entity key
        /// </summary>
        public string ContractEntityKey { get; set; }

        /// <summary>
        /// List of KeyValuePairs with property names and associated reference data keys (for lookup values), Key: PropertyName, Value: ReferenceDataKey
        /// e.g: new Dictionary<string, string>(){{"Currency","MarketCurrency"}, {"NominationUnits", "MarketNominationUnits"}}
        /// </summary>
        public IDictionary<string, string> PropertiesWithReferenceData { get; set; }

        /// <summary>
        /// List of KeyValuePairs with Properties which has different names than the reference entity names, Key: PropertyName, Value: reference entity Name
        /// e.g.: Product has DefaultCurve property which is of type Curve
        /// new Dictionary<string, string>(){{"DefaultCurve","Curve"}}
        /// </summary>
        public IDictionary<string, string> PropertiesWithDifferentEntityName { get; set; }

        /// <summary>
        /// if true then this entity type refers to some other entity type and hence needs to support an EmbeddedSearchResultsView
        /// </summary>
        public bool RefersTo { get; set; }

        /// <summary>
        /// List of entity types that are displayed in tabs on the EditView 
        /// </summary>
        public IList<string> ReferencedBy { get; set; }
    }
}