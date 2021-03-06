﻿namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.MDM;
    using EnergyTrading.Test;

    public class EntityMappingChecker : Checker<EntityMapping>
    {
        public EntityMappingChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.System).Id();
            Compare(x => x.MappingValue);
            Compare(x => x.IsMaster);
            Compare(x => x.IsDefault);
            Compare(x => x.Validity);
        }
    }
}