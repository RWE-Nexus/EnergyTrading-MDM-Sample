using System;
using System.Reflection;

namespace Common
{
    public class PropertyContainer
    {
        public bool IsParent { get; set; }
        public PropertyInfo UnderlyingProperty { get; set; }
        public string Name
        {
            get
            {
                return UnderlyingProperty.Name;
            }
        }

        public string EmitName
        {
            get
            {
                return !IsParent ? "Details." + UnderlyingProperty.Name : UnderlyingProperty.Name;
            }
        }

        public Type PropertyType
        {
            get { return UnderlyingProperty.PropertyType; }
        }
    }
}
