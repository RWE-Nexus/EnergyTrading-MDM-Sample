namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Mapping;

    public class LocationDetailsMapper : Mapper<EnergyTrading.MDM.Location, LocationDetails>
    {
        public override void Map(EnergyTrading.MDM.Location source, LocationDetails destination)
        {
            destination.Type = source.Type ?? string.Empty;
            destination.Name = source.Name;
            destination.Parent = source.Parent.CreateNexusEntityId(() => source.Parent.Name);
        }
    }
}		